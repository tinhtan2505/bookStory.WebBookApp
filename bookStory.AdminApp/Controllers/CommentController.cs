using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.AdminApp.Controllers;
using bookStory.ApiIntegration.Comment;
using bookStory.ApiIntegration.Translation;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace bookStory.AdminApp.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentApiClient _CommentApiClient;
        private readonly ITranslationApiClient _translationApiClient;
        private readonly IConfiguration _configuration;

        public CommentController(ICommentApiClient CommentApiClient,
            IConfiguration configuration,
            ITranslationApiClient translationApiClient)
        {
            _configuration = configuration;
            _CommentApiClient = CommentApiClient;
            _translationApiClient = translationApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? idtranslation, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageCommentPagingRequest()
            {
                IdTranslation = idtranslation,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
                //LanguageId = languageId
            };
            var data = await _CommentApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            var translations = await _translationApiClient.GetAll();
            ViewBag.Translations = translations.Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Id.ToString(),
                Selected = idtranslation.HasValue && idtranslation.Value == x.Id
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
            var result = await _CommentApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CommentCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _CommentApiClient.CreateComment(request);
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

            var Comment = await _CommentApiClient.GetById(id);
            var editVm = new CommentUpdateRequest()
            {
                Id = Comment.Id,
                UserId = Comment.UserId,
                IdTranslation = Comment.IdTranslation,
                Message = Comment.Message,
                DateComment = Comment.DateComment
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] CommentUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _CommentApiClient.UpdateComment(request);
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
            return View(new CommentDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CommentDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _CommentApiClient.DeleteComment(request.Id);
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