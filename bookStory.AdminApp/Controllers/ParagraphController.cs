using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.AdminApp.Controllers;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Paragraph;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Paragraps;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace ParagraphStory.AdminApp.Controllers
{
    public class ParagraphController : BaseController
    {
        private readonly IParagraphApiClient _ParagraphApiClient;
        private readonly IBookApiClient _bookApiClient;
        private readonly IConfiguration _configuration;

        public ParagraphController(IParagraphApiClient ParagraphApiClient,
            IConfiguration configuration,
            IBookApiClient bookApiClient)
        {
            _configuration = configuration;
            _ParagraphApiClient = ParagraphApiClient;
            _bookApiClient = bookApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? idbook, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageParagraphPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                IdBook = idbook
                //LanguageId = languageId
            };
            var data = await _ParagraphApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            var books = await _bookApiClient.GetAll();
            ViewBag.Books = books.Select(x => new SelectListItem()
            {
                Text = x.FileName,
                Value = x.Id.ToString(),
                Selected = idbook.HasValue && idbook.Value == x.Id
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
            var result = await _ParagraphApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? idbook)
        {
            var books = await _bookApiClient.GetAll();
            ViewBag.Books = books.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = idbook.HasValue && idbook.Value == x.Id
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ParagraphCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ParagraphApiClient.CreateParagraph(request);
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

            var Paragraph = await _ParagraphApiClient.GetById(id);
            var editVm = new ParagraphUpdateRequest()
            {
                Id = Paragraph.Id,
                IdBook = Paragraph.IdBook,
                Order = Paragraph.Order,
                Type = Paragraph.Type
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ParagraphUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ParagraphApiClient.UpdateParagraph(request);
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
            return View(new ParagraphDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ParagraphDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _ParagraphApiClient.DeleteParagraph(request.Id);
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