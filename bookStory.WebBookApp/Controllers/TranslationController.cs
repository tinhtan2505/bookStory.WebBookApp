using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Paragraph;
using bookStory.ApiIntegration.Translation;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Translations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace bookStory.WebBookApp.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationApiClient _TranslationApiClient;
        private readonly IParagraphApiClient _paragraphApiClient;
        private readonly IConfiguration _configuration;

        public TranslationController(ITranslationApiClient TranslationApiClient,
            IConfiguration configuration,
            IParagraphApiClient paragraphApiClient)
        {
            _configuration = configuration;
            _TranslationApiClient = TranslationApiClient;
            _paragraphApiClient = paragraphApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? idparagraph, int pageIndex = 1, int pageSize = 1)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageTranslationPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
                //LanguageId = languageId
            };
            var data = await _TranslationApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            var paragraphs = await _paragraphApiClient.GetAll();
            ViewBag.Paragraphs = paragraphs.Select(x => new SelectListItem()
            {
                Text = x.Order,
                Value = x.Id.ToString(),
                Selected = idparagraph.HasValue && idparagraph.Value == x.Id
            });
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _TranslationApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TranslationCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _TranslationApiClient.CreateTranslation(request);
            if (result)
            {
                TempData["result"] = "Thêm mới Sách thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới Sách thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var Translation = await _TranslationApiClient.GetById(id);
            var editVm = new TranslationUpdateRequest()
            {
                Id = Translation.Id,
                UserId = Translation.UserId,
                IdParagraph = Translation.IdParagraph,
                Text = Translation.Text,
                Rating = Translation.Rating,
                Date = Translation.Date
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] TranslationUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _TranslationApiClient.UpdateTranslation(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sách thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sách thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new TranslationDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TranslationDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _TranslationApiClient.DeleteTranslation(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}