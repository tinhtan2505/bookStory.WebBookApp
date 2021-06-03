using bookStory.ViewModels.Catalog.BookImages;
using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Books
{
    public interface IBookService
    {
        Task<int> Create(BookCreateRequest request);

        Task<int> Update(BookUpdateRequest request);

        Task<int> Delete(int IdBook);

        //Task<List<BookViewModel>> GetAll();
        Task<BookViewModel> GetById(int IdBook);

        Task<PagedResult<BookViewModel>> GetAllPaging(GetManageBookPagingRequest request);

        Task<int> AddImage(int IdBook, BookImageCreateRequest request);

        Task<int> RemoveImage(int IdImage);

        Task<int> UpdateImage(int IdImage, BookImageUpdateRequest request);

        Task<BookImageViewModel> GetImageById(int imageId);

        Task<List<BookImageViewModel>> GetListImages(int productId);

        Task<List<BookViewModel>> GetAll();

        Task<List<BookViewModel>> GetFeaturedProducts(int take);

        Task<List<BookViewModel>> GetLatestProducts(int take);
    }
}