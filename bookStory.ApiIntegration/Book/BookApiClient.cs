using bookStory.Utilities.Constants;
using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Book
{
    public class BookApiClient : BaseApiClient, IBookApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BookApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateBook(BookCreateRequest request)
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

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.FileName.ToString()), "fileName");
            requestContent.Add(new StringContent(request.Title.ToString()), "title");
            requestContent.Add(new StringContent(request.Author.ToString()), "author");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/books/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var data = await GetAsync<BookViewModel>($"/api/books/{id}");

            return data;
        }

        //public async Task<BookViewModel> GetById(int id)
        //{
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        //    var response = await client.GetAsync($"/api/books/{id}");
        //    var body = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<BookViewModel>(body);

        //    return JsonConvert.DeserializeObject<BookViewModel>(body);
        //}

        public async Task<PagedResult<BookViewModel>> GetPagings(GetManageBookPagingRequest request)
        {
            var data = await GetAsync<PagedResult<BookViewModel>>(
                $"/api/books/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<bool> UpdateBook(BookUpdateRequest request)
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

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            //requestContent.Add(new StringContent(request.Id.ToString()), "id");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FileName) ? "" : request.FileName.ToString()), "fileName");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Title) ? "" : request.Title.ToString()), "title");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Author) ? "" : request.Author.ToString()), "author");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/books/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<BookViewModel>> GetAll()
        {
            return await GetListAsync<BookViewModel>("/api/books");
        }

        public async Task<bool> DeleteBook(int id)
        {
            return await Delete($"/api/books/" + id);
        }

        public async Task<List<BookViewModel>> GetTops(int take)
        {
            var data = await GetListAsync<BookViewModel>($"/api/books/featured/{take}");
            return data;
        }

        public async Task<List<BookViewModel>> GetLatestProducts(int take)
        {
            var data = await GetListAsync<BookViewModel>($"/api/books/latest/{take}");
            return data;
        }
    }
}