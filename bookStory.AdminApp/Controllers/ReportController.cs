using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.AdminApp.Controllers;
using bookStory.ApiIntegration.Report;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bookStory.AdminApp.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportApiClient _ReportApiClient;
        private readonly IConfiguration _configuration;

        public ReportController(IReportApiClient ReportApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _ReportApiClient = ReportApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageReportPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
                //LanguageId = languageId
            };
            var data = await _ReportApiClient.GetPagings(request);
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
            var result = await _ReportApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ReportCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ReportApiClient.CreateReport(request);
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

            var Report = await _ReportApiClient.GetById(id);
            var editVm = new ReportUpdateRequest()
            {
                Id = Report.Id,
                UserId = Report.UserId,
                IdParagraph = Report.IdParagraph,
                Reason = Report.Reason
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ReportUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ReportApiClient.UpdateReport(request);
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
            return View(new ReportDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ReportDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _ReportApiClient.DeleteReport(request.Id);
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