using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Comment;
using bookStory.ApiIntegration.Language;
using bookStory.ApiIntegration.Paragraph;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.Rating;
using bookStory.ApiIntegration.Report;
using bookStory.ApiIntegration.Translation;
using bookStory.ApiIntegration.User;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Catalog.Projects;
using bookStory.ViewModels.Catalog.Ratings;
using bookStory.ViewModels.Catalog.Reports;
using bookStory.ViewModels.Catalog.Translations;
using bookStory.WebBookApp.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IRatingApiClient _ratingApiClient;
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IReportApiClient _ReportApiClient;
        private readonly ILanguageApiClient _languageApiClient;

        public BookController(ILogger<BookController> logger,
            IBookApiClient bookApiClient,
            IParagraphApiClient paragraphApiClient,
            ITranslationApiClient translationApiClient,
            ICommentApiClient commentApiClient,
            IUserApiClient userApiClient,
            IRatingApiClient ratingApiClient,
            IProjectApiClient projectApiClient,
            ILanguageApiClient languageApiClient,
            IReportApiClient ReportApiClient)
        {
            _logger = logger;
            _bookApiClient = bookApiClient;
            _paragraphApiClient = paragraphApiClient;
            _translationApiClient = translationApiClient;
            _commentApiClient = commentApiClient;
            _userApiClient = userApiClient;
            _ratingApiClient = ratingApiClient;
            _ProjectApiClient = projectApiClient;
            _languageApiClient = languageApiClient;
            _ReportApiClient = ReportApiClient;
        }

        public async Task<IActionResult> Index()
        {
            //var msg = _loc.GetLocalizedString("Vietnamese");
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
        public async Task<IActionResult> Detail(int id, int? idLanguage)
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var languages = await _languageApiClient.GetAll();
            ViewBag.Languages = languages.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = idLanguage.HasValue && idLanguage.Value.Equals(x.Id)
            });
            var book = await _bookApiClient.GetById(id);
            var pra = await _paragraphApiClient.GetPagings(new GetManageParagraphPagingRequest()
            {
                IdBook = id,
                PageIndex = 1,
                PageSize = 10
            });
            return View(new BookDetailViewModel()
            {
                Book = book,
                ListParagraphs = pra
            });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Detail([FromForm] BookDetailViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            if (request.Title != null || request.IdLanguage != null) { }
            var user = await _userApiClient.GetUsersName(request.UserName);
            ProjectCreateRequest create = new ProjectCreateRequest()
            {
                UserId = user.ResultObj.Id,
                IdBook = request.IdBook,
                Title = request.Title,
                Description = request.Description,
                IdLanguage = request.IdLanguage
            };
            var result = await _ProjectApiClient.CreateProject(create);
            if (result)
            {
                TempData["result"] = "Yêu cầu của bạn đã được chuyển đến Quản trị viên!";
                return RedirectToAction("Detail");
            }

            TempData["result"] = "Yêu cầu thất bại! Vui lòng kiểm tra lại dữ liệu!!";
            return RedirectToAction("Detail");
        }

        [HttpGet]
        public async Task<IActionResult> Translation(int id)
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
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
        public async Task<IActionResult> CreateReport([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            ReportCreateRequest create = new ReportCreateRequest()
            {
                UserId = user.ResultObj.Id,
                IdParagraph = request.IdParagraph,
                Reason = request.Reason
            };
            if (!ModelState.IsValid)
                return View(request);
            var result = await _ReportApiClient.CreateReport(create);
            if (result)
            {
                TempData["result"] = "Báo cáo thành công";
                return RedirectToAction("Translation", "Book", new { id = request.IdParagraph });
            }

            ModelState.AddModelError("", "Báo cáo thất bại");
            return View(request);
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
                Message = request.MessageCreate
            };
            if (!ModelState.IsValid)
                return View(request);
            var tran = await _translationApiClient.GetById(request.IdTranslation);
            var result = await _commentApiClient.CreateComment(create);
            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("BinhLuan", "Book", new { id = request.IdTranslation });
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditComment([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            CommentUpdateRequest create = new CommentUpdateRequest()
            {
                Id = request.IdComment,
                UserId = user.ResultObj.Id,
                IdTranslation = request.IdTranslation,
                Message = request.Message
            };
            if (!ModelState.IsValid)
                return View(request);
            var tran = await _translationApiClient.GetById(request.IdTranslation);
            var result = await _commentApiClient.UpdateComment(create);
            if (result)
            {
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("BinhLuan", "Book", new { id = request.IdTranslation });
            }

            ModelState.AddModelError("", "Cập nhật thất bại");
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTranslation([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            TranslationCreateRequest create = new TranslationCreateRequest()
            {
                UserId = user.ResultObj.Id,
                IdParagraph = request.IdParagraph,
                Text = request.Text,
                Rating = request.Rating
            };
            if (!ModelState.IsValid)
                return View(request);

            var result = await _translationApiClient.CreateTranslation(create);
            if (result)
            {
                TempData["result"] = "Thêm mới Sách thành công";
                return RedirectToAction("Translation", "Book", new { id = request.IdParagraph });
            }

            ModelState.AddModelError("", "Thêm mới Sách thất bại");
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditTranslation([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            TranslationUpdateRequest create = new TranslationUpdateRequest()
            {
                Id = request.IdTranslation,
                UserId = user.ResultObj.Id,
                IdParagraph = request.IdParagraph,
                Text = request.Text,
                Rating = request.Rating
            };
            if (!ModelState.IsValid)
                return View(request);

            var result = await _translationApiClient.UpdateTranslation(create);
            if (result)
            {
                TempData["result"] = "Cập nhật sách thành công";
                return RedirectToAction("BinhLuan", "Book", new { id = request.IdTranslation });
            }

            ModelState.AddModelError("", "Cập nhật sách thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(ParagraphDetailViewModel request)
        {
            if (!ModelState.IsValid)
                return View();
            var tran = await _translationApiClient.GetById(request.IdTranslation);
            var result = await _commentApiClient.DeleteComment(request.IdComment);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("BinhLuan", "Book", new { id = request.IdTranslation });
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTranslation(ParagraphDetailViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _translationApiClient.DeleteTranslation(request.IdTranslation);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Translation", "Book", new { id = request.IdParagraph });
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateXepHang([FromForm] ParagraphDetailViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);

            bool kt = false;
            int IdRating = 0;
            var ratings = await _ratingApiClient.GetAll();
            foreach (var r in ratings)
            {
                if (r.UserId == user.ResultObj.Id && r.IdTranslation == request.IdTranslation)
                {
                    kt = true;
                    IdRating = r.Id;
                    break;
                }
            }
            var result = false;
            if (kt == false)
            {
                RatingCreateRequest create = new RatingCreateRequest()
                {
                    UserId = user.ResultObj.Id,
                    IdTranslation = request.IdTranslation,
                    Vote = request.Vote
                };
                result = await _ratingApiClient.CreateRating(create);
            }
            else
            {
                RatingUpdateRequest edit = new RatingUpdateRequest()
                {
                    Id = IdRating,
                    UserId = user.ResultObj.Id,
                    IdTranslation = request.IdTranslation,
                    Vote = request.Vote
                };
                result = await _ratingApiClient.UpdateRating(edit);
            }

            if (result)
            {
                TempData["result"] = "Thêm mới Sách thành công";
                return RedirectToAction("BinhLuan", "Book", new { id = request.IdTranslation });
            }

            ModelState.AddModelError("", "Thêm mới Sách thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> BinhLuan(int id)
        {
            var tran = await _translationApiClient.GetById(id);
            var paragraph = await _paragraphApiClient.GetById(tran.IdParagraph);
            var book = await _bookApiClient.GetById(paragraph.IdBook);
            var com = await _commentApiClient.GetAll();

            return View(new ParagraphDetailViewModel()
            {
                Paragraph = paragraph,
                Book = book,
                ListComments = com,
                Translation = tran
            });
        }
    }
}