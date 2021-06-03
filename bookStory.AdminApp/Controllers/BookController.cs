using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Book;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bookStory.AdminApp.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookApiClient _bookApiClient;
        private readonly IConfiguration _configuration;

        public BookController(IBookApiClient bookApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _bookApiClient = bookApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageBookPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
                //LanguageId = languageId
            };
            var data = await _bookApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _bookApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] BookCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _bookApiClient.CreateBook(request);
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

            var book = await _bookApiClient.GetById(id);
            var editVm = new BookUpdateRequest()
            {
                Id = book.Id,
                FileName = book.FileName,
                Title = book.Title,
                Author = book.Author
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] BookUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _bookApiClient.UpdateBook(request);
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
            return View(new BookDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BookDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _bookApiClient.DeleteBook(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}