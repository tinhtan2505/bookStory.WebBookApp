using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Chats;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Chat
{
    public class ChatApiClient : BaseApiClient, IChatApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ChatApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateChat(ChatCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.UserIdSender.ToString()), "userIdSender");
            requestContent.Add(new StringContent(request.UserIdReceiver.ToString()), "userIdReceiver");
            requestContent.Add(new StringContent(request.Message.ToString()), "message");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/chats/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ChatViewModel>> GetAll()
        {
            return await GetListAsync<ChatViewModel>("/api/chats");
        }

        public async Task<ChatViewModel> GetById(int id)
        {
            var data = await GetAsync<ChatViewModel>($"/api/chats/{id}");

            return data;
        }

        public async Task<PagedResult<ChatViewModel>> GetPagings(GetManageChatPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ChatViewModel>>(
                $"/api/chats/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<bool> UpdateChat(ChatUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.UserIdSender.ToString()), "userIdSender");
            requestContent.Add(new StringContent(request.UserIdReceiver.ToString()), "userIdReceiver");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Message) ? "" : request.Message.ToString()), "message");
            requestContent.Add(new StringContent(request.DateComment.ToString()), "dateComment");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/chats/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteChat(int id)
        {
            return await Delete($"/api/chats/" + id);
        }
    }
}