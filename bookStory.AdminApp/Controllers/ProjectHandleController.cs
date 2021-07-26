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
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ApiIntegration.Paragraph;

namespace bookStory.AdminApp.Controllers
{
    [Authorize]
    public class ProjectHandleController : Controller
    {
        private readonly ILogger<ProjectHandleController> _logger;
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IBookApiClient _bookApiClient;
        private readonly IReportApiClient _ReportApiClient;
        private readonly IParagraphApiClient _ParagraphApiClient;

        public ProjectHandleController(ILogger<ProjectHandleController> logger,
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
        public async Task<IActionResult> CreateParagraph(int? idbook)
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
        public async Task<IActionResult> CreateParagraph([FromForm] ParagraphCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ParagraphApiClient.CreateParagraph(request);
            if (result)
            {
                TempData["result"] = "Thêm mới Dự án thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới Dự án thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _ProjectApiClient.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Index(string keywordP, string keywordR, int? idBook, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var project = await _ProjectApiClient.GetPagings(new GetManageProjectPagingRequest()
            {
                Keyword = keywordP,
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
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _ProjectApiClient.GetById(id);
            return View(new ProjectDeleteRequest()
            {
                Id = id,
                Title = result.Title,
                Description = result.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProject(ProjectDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _ProjectApiClient.DeleteProject(request.Id);
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