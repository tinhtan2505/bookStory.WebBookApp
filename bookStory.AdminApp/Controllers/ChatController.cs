using bookStory.AdminApp.Hubs;
using bookStory.ApiIntegration.Chat;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Chats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.AdminApp.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatApiClient _CommentApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<ChatSignlR> _signalrHub;

        public ChatController(IChatApiClient CommentApiClient,
            IConfiguration configuration,
            IHubContext<ChatSignlR> signalrHub)
        {
            _configuration = configuration;
            _CommentApiClient = CommentApiClient;
            _signalrHub = signalrHub;
        }

        [HttpGet]
        public async Task<IActionResult> GetChat()
        {
            var com = await _CommentApiClient.GetAll();

            return Ok(com);
        }

        public async Task<IActionResult> Index(string keyword, int? idtranslation, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageChatPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _CommentApiClient.GetPagings(request);
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
            var result = await _CommentApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ChatCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _CommentApiClient.CreateChat(request);
            await _signalrHub.Clients.All.SendAsync("LoadChat");
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

            var Comment = await _CommentApiClient.GetById(id);
            var editVm = new ChatUpdateRequest()
            {
                Id = Comment.Id,
                UserIdSender = Comment.UserIdSender,
                UserIdReceiver = Comment.UserIdReceiver,
                Message = Comment.Message,
                DateComment = Comment.DateComment
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ChatUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _CommentApiClient.UpdateChat(request);
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
            return View(new ChatDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ChatDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _CommentApiClient.DeleteChat(request.Id);
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