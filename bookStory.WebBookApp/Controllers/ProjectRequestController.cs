using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.User;
using bookStory.ViewModels.Catalog.Projects;
using bookStory.WebBookApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public ProjectRequestController(IProjectApiClient ProjectApiClient,
            IConfiguration configuration,
            IBookApiClient bookApiClient,
            IUserApiClient userApiClient)
        {
            _configuration = configuration;
            _ProjectApiClient = ProjectApiClient;
            _bookApiClient = bookApiClient;
            _userApiClient = userApiClient;
        }

        [HttpGet]
        public IActionResult Request()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
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