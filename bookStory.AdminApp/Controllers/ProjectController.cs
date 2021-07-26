using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Language;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.User;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace bookStory.AdminApp.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectApiClient _ProjectApiClient;
        private readonly IConfiguration _configuration;
        private readonly IBookApiClient _bookApiClient;
        private readonly ILanguageApiClient _languageApiClient;
        private readonly IUserApiClient _userApiClient;

        public ProjectController(IProjectApiClient ProjectApiClient,
            IConfiguration configuration,
            IBookApiClient bookApiClient,
            ILanguageApiClient languageApiClient,
            IUserApiClient userApiClient)
        {
            _configuration = configuration;
            _ProjectApiClient = ProjectApiClient;
            _bookApiClient = bookApiClient;
            _languageApiClient = languageApiClient;
            _userApiClient = userApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? idBook, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageProjectPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                IdBook = idBook
                //LanguageId = languageId
            };
            var data = await _ProjectApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

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
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _ProjectApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? idUser, int? idBook, int? idLanguage)
        {
            var users = await _userApiClient.GetAll();
            ViewBag.Users = users.Select(x => new SelectListItem()
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString(),
                Selected = idUser.HasValue && idUser.Value.Equals(x.Id.ToString())
                //Selected = new Guid()
            });
            var books = await _bookApiClient.GetAll();
            ViewBag.Books = books.Select(x => new SelectListItem()
            {
                Text = x.Title,
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
        public async Task<IActionResult> Create([FromForm] ProjectCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ProjectApiClient.CreateProject(request);
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

            var Project = await _ProjectApiClient.GetById(id);
            var editVm = new ProjectUpdateRequest()
            {
                Id = Project.Id,
                IdLanguage = Project.IdLanguage,
                IdBook = Project.IdBook,
                UserId = Project.UserId,
                Title = Project.Title,
                Description = Project.Description,
                Status = Project.Status,
                DateProject = DateTime.Now
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ProjectUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _ProjectApiClient.UpdateProject(request);
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
            return View(new ProjectDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectDeleteRequest request)
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