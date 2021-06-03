using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.AdminApp.Controllers;
using bookStory.ApiIntegration.Rating;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Ratings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bookStory.AdminApp.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingApiClient _RatingApiClient;
        private readonly IConfiguration _configuration;

        public RatingController(IRatingApiClient RatingApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _RatingApiClient = RatingApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageRatingPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
                //LanguageId = languageId
            };
            var data = await _RatingApiClient.GetPagings(request);
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
            var result = await _RatingApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] RatingCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _RatingApiClient.CreateRating(request);
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

            var Rating = await _RatingApiClient.GetById(id);
            var editVm = new RatingUpdateRequest()
            {
                Id = Rating.Id,
                UserId = Rating.UserId,
                IdTranslation = Rating.IdTranslation,
                Vote = Rating.Vote
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] RatingUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _RatingApiClient.UpdateRating(request);
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
            return View(new RatingDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RatingDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _RatingApiClient.DeleteRating(request.Id);
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