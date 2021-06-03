using bookStory.ApiIntegration;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Paragraps;
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

namespace bookStory.ApiIntegration.Paragraph
{
    public class ParagraphApiClient : BaseApiClient, IParagraphApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ParagraphApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ParagraphViewModel>> GetAll()
        {
            return await GetListAsync<ParagraphViewModel>("/api/paragraphs");
        }

        public async Task<bool> CreateParagraph(ParagraphCreateRequest request)
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

            requestContent.Add(new StringContent(request.IdBook.ToString()), "idBook");
            requestContent.Add(new StringContent(request.Order.ToString()), "order");
            requestContent.Add(new StringContent(request.Type.ToString()), "type");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/paragraphs/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ParagraphViewModel> GetById(int id)
        {
            var data = await GetAsync<ParagraphViewModel>($"/api/paragraphs/{id}");

            return data;
        }

        public async Task<PagedResult<ParagraphViewModel>> GetPagings(GetManageParagraphPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ParagraphViewModel>>(
                $"/api/paragraphs/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&idbook={request.IdBook}");

            return data;
        }

        public async Task<bool> UpdateParagraph(ParagraphUpdateRequest request)
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

            requestContent.Add(new StringContent(request.IdBook.ToString()), "idBook");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Order) ? "" : request.Order.ToString()), "order");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Type) ? "" : request.Type.ToString()), "type");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/paragraphs/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteParagraph(int id)
        {
            return await Delete($"/api/paragraphs/" + id);
        }
    }
}