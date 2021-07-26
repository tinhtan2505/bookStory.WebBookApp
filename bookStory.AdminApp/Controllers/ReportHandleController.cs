using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bookStory.AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using bookStory.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using bookStory.ViewModels.Catalog.Projects;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.Book;
using Microsoft.AspNetCore.Mvc.Rendering;
using bookStory.ViewModels.Catalog.Reports;
using bookStory.ApiIntegration.Report;
using bookStory.ApiIntegration.Paragraph;
using bookStory.ViewModels.Catalog.Paragraps;

namespace bookStory.AdminApp.Controllers
{
    [Authorize]
    public class ReportHandleController : Controller
    {
        private readonly ILogger<ReportHandleController> _logger;
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IBookApiClient _bookApiClient;
        private readonly IReportApiClient _ReportApiClient;
        private readonly IParagraphApiClient _ParagraphApiClient;

        public ReportHandleController(ILogger<ReportHandleController> logger,
            IProjectApiClient ProjectApiClient,
            IBookApiClient bookApiClient,
            IReportApiClient ReportApiClient,
            IParagraphApiClient ParagraphApiClient)
        {
            _logger = logger;
            _ProjectApiClient = ProjectApiClient;
            _bookApiClient = bookApiClient;
            _ReportApiClient = ReportApiClient;
            _ParagraphApiClient = ParagraphApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> DetailsParagraph(int id)
        {
            var result = await _ParagraphApiClient.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Index(string keywordP, string keywordR, int? idBook, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var report = await _ReportApiClient.GetPagings(new GetManageReportPagingRequest()
            {
                Keyword = keywordR,
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            ViewBag.KeywordP = keywordP;

            var books = await _bookApiClient.GetAll();
            ViewBag.Books = books.Select(x => new SelectListItem()
            {
                Text = x.FileName,
                Value = x.Id.ToString(),
                Selected = idBook.HasValue && idBook.Value == x.Id
            });
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> EditParagraph(int id)
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
        public async Task<IActionResult> EditParagraph([FromForm] ParagraphUpdateRequest request)
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
        public async Task<IActionResult> DeleteParagraph(int id)
        {
            var result = await _ParagraphApiClient.GetById(id);
            return View(new ParagraphDeleteRequest()
            {
                Id = id,
                TitleBook = result.TitleBook,
                Order = result.Order
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteParagraph(ParagraphDeleteRequest request)
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

        [HttpGet]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var result = await _ReportApiClient.GetById(id);
            return View(new ReportDeleteRequest()
            {
                Id = id,
                TitleBook = result.TitleBook,
                Order = result.Order,
                Reason = result.Reason
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReport(ReportDeleteRequest request)
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