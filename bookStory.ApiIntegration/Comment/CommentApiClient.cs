using bookStory.ApiIntegration;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Comments;
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

namespace bookStory.ApiIntegration.Comment
{
    public class CommentApiClient : BaseApiClient, ICommentApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CommentApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateComment(CommentCreateRequest request)
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

            requestContent.Add(new StringContent(request.UserId.ToString()), "userId");
            requestContent.Add(new StringContent(request.IdTranslation.ToString()), "idTranslation");
            requestContent.Add(new StringContent(request.Message.ToString()), "message");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/comments/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CommentViewModel>> GetAll()
        {
            return await GetListAsync<CommentViewModel>("/api/comments");
        }

        public async Task<CommentViewModel> GetById(int id)
        {
            var data = await GetAsync<CommentViewModel>($"/api/comments/{id}");

            return data;
        }

        public async Task<PagedResult<CommentViewModel>> GetPagings(GetManageCommentPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CommentViewModel>>(
                $"/api/comments/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&idtranslation={request.IdTranslation}");

            return data;
        }

        public async Task<bool> UpdateComment(CommentUpdateRequest request)
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

            requestContent.Add(new StringContent(request.UserId.ToString()), "userId");
            requestContent.Add(new StringContent(request.IdTranslation.ToString()), "idTranslation");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Message) ? "" : request.Message.ToString()), "message");
            requestContent.Add(new StringContent(request.DateComment.ToString()), "dateComment");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/comments/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteComment(int id)
        {
            return await Delete($"/api/comments/" + id);
        }
    }
}