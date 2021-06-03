using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Comment;
using bookStory.ApiIntegration.Paragraph;
using bookStory.ApiIntegration.Translation;
using bookStory.ApiIntegration.User;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Catalog.Translations;
using bookStory.WebBookApp.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bookStory.WebBookApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly IUserApiClient _userApiClient;

        private readonly IBookApiClient _bookApiClient;
        private readonly IParagraphApiClient _paragraphApiClient;
        private readonly ITranslationApiClient _translationApiClient;
        private readonly ICommentApiClient _commentApiClient;

        public BookController(ILogger<BookController> logger,
            IBookApiClient bookApiClient,
            IParagraphApiClient paragraphApiClient,
            ITranslationApiClient translationApiClient,
            ICommentApiClient commentApiClient,
            IUserApiClient userApiClient)
        {
            _logger = logger;
            _bookApiClient = bookApiClient;
            _paragraphApiClient = paragraphApiClient;
            _translationApiClient = translationApiClient;
            _commentApiClient = commentApiClient;
            _userApiClient = userApiClient;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUsersName(string UserName)
        //{
        //    var roleAssignRequest = await _userApiClient.GetUsersName(UserName);
        //    return View(roleAssignRequest);
        //}
        public async Task<IActionResult> Index()
        {
            //var msg = _loc.GetLocalizedString("Vietnamese");
            var user = await _userApiClient.GetUsersName("tinhtan");
            var culture = CultureInfo.CurrentCulture.Name;
            var viewModel = new HomeViewModel
            {
                //Slides = await _slideApiClient.GetAll(),
                FeaturedProducts = await _bookApiClient.GetFeaturedProducts(SystemConstants.ProductSettings.NumberOfFeaturedProducts),
                LatestProducts = await _bookApiClient.GetLatestProducts(SystemConstants.ProductSettings.NumberOfLatestProducts),
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _bookApiClient.GetById(id);
            var pra = await _paragraphApiClient.GetPagings(new GetManageParagraphPagingRequest()
            {
                IdBook = id,
                PageIndex = 1,
                PageSize = 10
            });
            //var pra = await _paragraphApiClient.GetA
            return View(new BookDetailViewModel()
            {
                Book = book,
                ListParagraphs = pra
            });
        }

        [HttpGet]
        public async Task<IActionResult> Translation(int id)
        {
            var paragraph = await _paragraphApiClient.GetById(id);

            var book = await _bookApiClient.GetById(paragraph.IdBook);
            var com = await _commentApiClient.GetAll();
            var translations = await _translationApiClient.GetPagings(new GetManageTranslationPagingRequest()
            {
                IdParagraph = id,
                PageIndex = 1,
                PageSize = 10
            });
            return View(new ParagraphDetailViewModel()
            {
                Paragraph = paragraph,
                Book = book,
                ListComments = com,
                ListTranslations = translations
            });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateComment([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            CommentCreateRequest create = new CommentCreateRequest()
            {
                UserId = user.ResultObj.Id,
                IdTranslation = request.IdTranslation,
                Message = request.Message
            };
            request.UserId = user.ResultObj.Id;
            if (!ModelState.IsValid)
                return View(request);
            var tran = await _translationApiClient.GetById(request.IdTranslation);
            var result = await _commentApiClient.CreateComment(create);
            if (result)
            {
                TempData["result"] = "Thêm mới Sách thành công";
                return RedirectToAction("Translation", "Book", new { id = tran.IdParagraph });
            }

            ModelState.AddModelError("", "Thêm mới Sách thất bại");
            return View(request);
        }
    }
}