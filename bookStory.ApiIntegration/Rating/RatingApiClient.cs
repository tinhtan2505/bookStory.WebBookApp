using bookStory.ApiIntegration;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Ratings;
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

namespace bookStory.ApiIntegration.Rating
{
    public class RatingApiClient : BaseApiClient, IRatingApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RatingApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<RatingViewModel>> GetAll()
        {
            return await GetListAsync<RatingViewModel>("/api/ratings");
        }

        public async Task<bool> CreateRating(RatingCreateRequest request)
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
            requestContent.Add(new StringContent(request.Vote.ToString()), "vote");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/ratings/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<RatingViewModel> GetById(int id)
        {
            var data = await GetAsync<RatingViewModel>($"/api/ratings/{id}");

            return data;
        }

        public async Task<RatingViewModel> GetRating(Guid keywordUserId, int keywordIdTranslation)
        {
            //var data1 = await GetAsync<RatingViewModel>($"/api/ratings/&userId = {keywordUserId}" + $"&idTran={keywordIdTranslation}");
            var data = await GetAsync<RatingViewModel>(
                $"/api/ratings/getrating?UserId={keywordUserId}" +
                $"&IdTranslation={keywordIdTranslation}");

            return data;
        }

        public async Task<PagedResult<RatingViewModel>> GetPagings(GetManageRatingPagingRequest request)
        {
            var data = await GetAsync<PagedResult<RatingViewModel>>(
                $"/api/ratings/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<bool> UpdateRating(RatingUpdateRequest request)
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
            requestContent.Add(new StringContent(request.Vote.ToString()), "vote");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/ratings/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRating(int id)
        {
            return await Delete($"/api/ratings/" + id);
        }
    }
}