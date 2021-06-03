using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Book
{
    public interface IBookApiClient
    {
        Task<PagedResult<BookViewModel>> GetPagings(GetManageBookPagingRequest request);

        Task<bool> CreateBook(BookCreateRequest request);

        Task<bool> UpdateBook(BookUpdateRequest request);

        Task<BookViewModel> GetById(int id);

        Task<bool> DeleteBook(int id);

        Task<List<BookViewModel>> GetAll();

        Task<List<BookViewModel>> GetFeaturedProducts(int take);

        Task<List<BookViewModel>> GetLatestProducts(int take);
    }
}