using bookStory.ApiIntegration;
using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Reports;
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

namespace bookStory.ApiIntegration.Report
{
    public class ReportApiClient : BaseApiClient, IReportApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ReportApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateReport(ReportCreateRequest request)
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
            requestContent.Add(new StringContent(request.Reason.ToString()), "reason");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/reports/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ReportViewModel> GetById(int id)
        {
            var data = await GetAsync<ReportViewModel>($"/api/reports/{id}");

            return data;
        }

        public async Task<PagedResult<ReportViewModel>> GetPagings(GetManageReportPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ReportViewModel>>(
                $"/api/reports/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<bool> UpdateReport(ReportUpdateRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Reason) ? "" : request.Reason.ToString()), "reason");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/reports/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteReport(int id)
        {
            return await Delete($"/api/reports/" + id);
        }
    }
}