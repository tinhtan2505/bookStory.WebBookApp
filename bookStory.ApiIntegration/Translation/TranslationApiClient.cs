using bookStory.ApiIntegration;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Translations;
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

namespace bookStory.ApiIntegration.Translation
{
    public class TranslationApiClient : BaseApiClient, ITranslationApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TranslationApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateTranslation(TranslationCreateRequest request)
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
            requestContent.Add(new StringContent(request.IdParagraph.ToString()), "idParagraph");
            requestContent.Add(new StringContent(request.Text.ToString()), "text");
            requestContent.Add(new StringContent(request.Rating.ToString()), "rating");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/translations/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TranslationViewModel>> GetAll()
        {
            return await GetListAsync<TranslationViewModel>("/api/translations");
        }

        public async Task<TranslationViewModel> GetById(int id)
        {
            var data = await GetAsync<TranslationViewModel>($"/api/translations/{id}");

            return data;
        }

        public async Task<PagedResult<TranslationViewModel>> GetPagings(GetManageTranslationPagingRequest request)
        {
            var data = await GetAsync<PagedResult<TranslationViewModel>>(
                $"/api/translations/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&idparagraph={request.IdParagraph}");

            return data;
        }

        public async Task<bool> UpdateTranslation(TranslationUpdateRequest request)
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
            requestContent.Add(new StringContent(request.IdParagraph.ToString()), "idParagraph");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Text) ? "" : request.Text.ToString()), "text");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Rating) ? "" : request.Rating.ToString()), "rating");
            requestContent.Add(new StringContent(request.Date.ToString()), "date");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/translations/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTranslation(int id)
        {
            return await Delete($"/api/translations/" + id);
        }
    }
}