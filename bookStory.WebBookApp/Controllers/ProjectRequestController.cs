using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Language;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.User;
using bookStory.ViewModels.Catalog.Projects;
using bookStory.WebBookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Controllers
{
    public class ProjectRequestController : Controller
    {
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IConfiguration _configuration;
        private readonly IBookApiClient _bookApiClient;
        private readonly IUserApiClient _userApiClient;
        private readonly ILanguageApiClient _languageApiClient;

        public ProjectRequestController(IProjectApiClient ProjectApiClient,
            IConfiguration configuration,
            IBookApiClient bookApiClient,
            IUserApiClient userApiClient,
            ILanguageApiClient languageApiClient)
        {
            _configuration = configuration;
            _ProjectApiClient = ProjectApiClient;
            _bookApiClient = bookApiClient;
            _userApiClient = userApiClient;
            _languageApiClient = languageApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Request(int? idBook, int? idLanguage)
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var books = await _bookApiClient.GetAll();
            ViewBag.Books = books.Select(x => new SelectListItem()
            {
                Text = x.FileName,
                Value = x.Id.ToString(),
                Selected = idBook.HasValue && idBook.Value == x.Id
            });
            var languages = await _languageApiClient.GetAll();
            ViewBag.Languages = languages.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = idLanguage.HasValue && idLanguage.Value.Equals(x.Id)
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Request([FromForm] ProjectRequestViewModel request)
        {
            var user = await _userApiClient.GetUsersName(request.UserName);
            ProjectCreateRequest create = new ProjectCreateRequest()
            {
                UserId = user.ResultObj.Id,
                IdBook = request.IdBook,
                Title = request.Title,
                Description = request.Description,
                IdLanguage = request.IdLanguage
            };
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ProjectApiClient.CreateProject(create);
            if (result)
            {
                TempData["result"] = "Yêu cầu của bạn đã được chuyển đến Quản trị viên";
                return RedirectToAction("Request");
            }

            ModelState.AddModelError("", "Yêu cầu thất bại");
            return View(request);
        }
    }
}