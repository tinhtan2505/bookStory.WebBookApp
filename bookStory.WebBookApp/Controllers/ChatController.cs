using bookStory.ApiIntegration.Chat;
using bookStory.ApiIntegration.User;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Chats;
using bookStory.WebBookApp.Hubs;
using bookStory.WebBookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.WebBookApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatApiClient _chatApiClient;
        private readonly IConfiguration _configuration;
        private readonly IUserApiClient _userApiClient;
        private readonly IHubContext<ChatSignlR> _signalrHub;

        public ChatController(IChatApiClient CommentApiClient,
            IConfiguration configuration,
            IUserApiClient userApiClient,
            IHubContext<ChatSignlR> signalrHub)
        {
            _configuration = configuration;
            _chatApiClient = CommentApiClient;
            _signalrHub = signalrHub;
            _userApiClient = userApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetChat()
        {
            var com = await _chatApiClient.GetAll();

            return Ok(com);
        }

        //public async Task<IActionResult> Index(string keyword, int? idtranslation, int pageIndex = 1, int pageSize = 10)
        //{
        //    var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

        //    var request = new GetManageChatPagingRequest()
        //    {
        //        Keyword = keyword,
        //        PageIndex = pageIndex,
        //        PageSize = pageSize
        //    };
        //    var data = await _chatApiClient.GetPagings(request);
        //    ViewBag.Keyword = keyword;
        //    if (TempData["result"] != null)
        //    {
        //        ViewBag.SuccessMsg = TempData["result"];
        //    }
        //    return View(data);
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var chats = await _chatApiClient.GetAll();
            return View(new ChatDetailViewModel()
            {
                ListChats = chats
            });
        }

        public async Task<IActionResult> IndexPrivate(string username2)
        {
            var chats = await _chatApiClient.GetAll();
            ViewBag.NguoiNhan2 = username2;
            var iduser2 = await _userApiClient.GetUsersName(username2);
            ViewBag.HoTen2 = iduser2.ResultObj.FirstName + iduser2.ResultObj.LastName;
            return View(new ChatDetailViewModel()
            {
                ListChats = chats
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _chatApiClient.GetById(id);
            return View(result);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ChatDetailViewModel request)
        {
            var iduser1 = await _userApiClient.GetUsersName(request.UserName1);
            var iduser2 = await _userApiClient.GetUsersName(request.UserName2);
            ChatCreateRequest create = new ChatCreateRequest()
            {
                UserIdSender = iduser1.ResultObj.Id,
                UserIdReceiver = iduser2.ResultObj.Id,
                Message = request.Message,
            };
            if (!ModelState.IsValid)
                return View(request);

            var result = await _chatApiClient.CreateChat(create);
            await _signalrHub.Clients.All.SendAsync("LoadListChat");
            if (result)
            {
                TempData["result"] = "Thêm mới Chat thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới Chat thất bại");
            return View(request);
        }

        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> Create(ChatDetailViewModel request)
        //{
        //    ChatCreateRequest create = new ChatCreateRequest()
        //    {
        //        UserIdSender = request.UserIdSender,
        //        UserIdReceiver = request.UserIdReceiver,
        //        Message = request.Message,
        //    };
        //    if (!ModelState.IsValid)
        //        return View(request);
        //    var result = await _chatApiClient.CreateChat(create);
        //    //await _signalrHub.Clients.All.SendAsync("LoadChat");
        //    if (result)
        //    {
        //        TempData["result"] = "Thêm mới Chat thành công";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", "Thêm mới Chat thất bại");
        //    return View(request);
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var Comment = await _chatApiClient.GetById(id);
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

            var result = await _chatApiClient.UpdateChat(request);
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

            var result = await _chatApiClient.DeleteChat(request.Id);
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