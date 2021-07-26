using System;
using System.Collections.Generic;
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

namespace bookStory.AdminApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IBookApiClient _bookApiClient;
        private readonly IReportApiClient _ReportApiClient;

        public HomeController(ILogger<HomeController> logger,
            IProjectApiClient ProjectApiClient,
            IBookApiClient bookApiClient,
            IReportApiClient ReportApiClient)
        {
            _logger = logger;
            _ProjectApiClient = ProjectApiClient;
            _bookApiClient = bookApiClient;
            _ReportApiClient = ReportApiClient;
        }

        //public IActionResult Index()
        //{
        //    var user = User.Identity.Name;
        //    return View();
        //}
        public async Task<IActionResult> Index(string keywordP, string keywordR, int? idBook, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var project = await _ProjectApiClient.GetPagings(new GetManageProjectPagingRequest()
            {
                Keyword = keywordP,
                PageIndex = pageIndex,
                PageSize = pageSize,
                IdBook = idBook
            });
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
            //return View(project);
            return View(new HomeViewModel()
            {
                ListProjects = project,
                ListReports = report
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return RedirectToAction("Index");
        }
    }
}