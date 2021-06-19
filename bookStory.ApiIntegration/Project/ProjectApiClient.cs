using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Projects;
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

namespace bookStory.ApiIntegration.Project
{
    public class ProjectApiClient : BaseApiClient, IProjectApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProjectApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateProject(ProjectCreateRequest request)
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

            requestContent.Add(new StringContent(request.IdLanguage.ToString()), "idLanguage");
            requestContent.Add(new StringContent(request.IdBook.ToString()), "idBook");
            requestContent.Add(new StringContent(request.UserId.ToString()), "userId");
            //requestContent.Add(new StringContent(request.Title.ToString()), "title");
            //requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()), "title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            //requestContent.Add(new StringContent(request.Status.ToString()), "status");
            //requestContent.Add(new StringContent(request.DateProject.ToString()), "dateProject");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/projects/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ProjectViewModel> GetById(int id)
        {
            var data = await GetAsync<ProjectViewModel>($"/api/projects/{id}");

            return data;
        }

        public async Task<PagedResult<ProjectViewModel>> GetPagings(GetManageProjectPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProjectViewModel>>(
                $"/api/projects/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&idBook={request.IdBook}");

            return data;
        }

        public async Task<bool> UpdateProject(ProjectUpdateRequest request)
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

            //requestContent.Add(new StringContent(languageId), "languageId");
            requestContent.Add(new StringContent(request.IdLanguage.ToString()), "idLanguage");
            requestContent.Add(new StringContent(request.IdBook.ToString()), "idBook");
            requestContent.Add(new StringContent(request.UserId.ToString()), "userId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()), "title");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync($"/api/projects/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProject(int id)
        {
            return await Delete($"/api/projects/" + id);
        }
    }
}