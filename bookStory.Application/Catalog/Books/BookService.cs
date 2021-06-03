using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.BookImages;
using bookStory.ViewModels.Catalog.Books;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Books
{
    public class BookService : IBookService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public BookService(bookStoryDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<BookViewModel>> GetAll()
        {
            var query = from b in _context.Books
                        select b;
            var data = await query.Select(x => new BookViewModel()
            {
                Id = x.Id,
                FileName = x.FileName,
                Title = x.Title,
                Author = x.Author,
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(BookCreateRequest request)
        {
            var book = new Book()
            {
                FileName = request.FileName,
                Title = request.Title,
                Author = request.Author
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                book.BookImages = new List<BookImage>()
                {
                    new BookImage()
                    {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<int> Delete(int IdBook)
        {
            var book = await _context.Books.FindAsync(IdBook);
            if (book == null) throw new BookException($"Khong the tim thay product: {IdBook}");

            var images = _context.BookImages.Where(i => i.IdBook == IdBook);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<BookViewModel>> GetAllPaging(GetManageBookPagingRequest request)
        {
            var query = from b in _context.Books
                        select b;
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.FileName.Contains(request.Keyword));

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new BookViewModel()
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    Title = x.Title,
                    Author = x.Author
                }).ToListAsync();

            var pagedResult = new PagedResult<BookViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<BookViewModel> GetById(int IdBook)
        {
            var book = await _context.Books.FindAsync(IdBook);
            var image = await _context.BookImages.Where(x => x.IdBook == IdBook && x.IsDefault == true).FirstOrDefaultAsync();
            var bookVM = new BookViewModel()
            {
                Id = book.Id,
                FileName = book.FileName,
                Title = book.Title,
                Author = book.Author,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return bookVM;
        }

        public async Task<int> Update(BookUpdateRequest request)
        {
            var book = await _context.Books.FindAsync(request.Id);
            //var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id
            //&& x.LanguageId == request.LanguageId);

            if (book == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            book.FileName = request.FileName;
            book.Title = request.Title;
            book.Author = request.Author;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.BookImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.IdBook == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.DateCreated = DateTime.Now;
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.BookImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<BookImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.BookImages.FindAsync(imageId);
            if (image == null)
                throw new BookException($"Cannot find an image with id {imageId}");

            var viewModel = new BookImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                IdBook = image.IdBook,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<BookImageViewModel>> GetListImages(int IdBook)
        {
            return await _context.BookImages.Where(x => x.IdBook == IdBook)
                .Select(i => new BookImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    IdBook = i.IdBook,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> AddImage(int IdBook, BookImageCreateRequest request)
        {
            var bookImage = new BookImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                IdBook = IdBook,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                bookImage.ImagePath = await this.SaveFile(request.ImageFile);
                bookImage.FileSize = request.ImageFile.Length;
            }
            _context.BookImages.Add(bookImage);
            await _context.SaveChangesAsync();
            return bookImage.Id;
        }

        public async Task<int> UpdateImage(int IdImage, BookImageUpdateRequest request)
        {
            var bookImage = await _context.BookImages.FindAsync(IdImage);
            if (bookImage == null)
                throw new BookException($"Cannot find an image with id {IdImage}");

            if (request.ImageFile != null)
            {
                bookImage.Caption = request.Caption;
                bookImage.IsDefault = request.IsDefault;
                bookImage.SortOrder = request.SortOrder;
                bookImage.ImagePath = await this.SaveFile(request.ImageFile);
                bookImage.FileSize = request.ImageFile.Length;
            }
            _context.BookImages.Update(bookImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveImage(int IdImage)
        {
            var bookImage = await _context.BookImages.FindAsync(IdImage);
            if (bookImage == null)
                throw new BookException($"Cannot find an image with id {IdImage}");
            _context.BookImages.Remove(bookImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<BookViewModel>> GetFeaturedProducts(int take)
        {
            var query = from b in _context.Books
                        join bi in _context.BookImages on b.Id equals bi.IdBook into bbi
                        from bi in bbi.DefaultIfEmpty()
                        where (bi == null || bi.IsDefault == true)
                        select new { b, bi };

            var data = await query.OrderByDescending(x => x.b.Title).Take(take)
                .Select(x => new BookViewModel()
                {
                    Id = x.b.Id,
                    FileName = x.b.FileName,
                    Title = x.b.Title,
                    Author = x.b.Author,
                    ThumbnailImage = x.bi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<List<BookViewModel>> GetLatestProducts(int take)
        {
            //1. Select join
            var query = from b in _context.Books
                        join bi in _context.BookImages on b.Id equals bi.IdBook into bbi
                        from bi in bbi.DefaultIfEmpty()
                        where (bi == null || bi.IsDefault == true)
                        select new { b, bi };

            var data = await query.OrderByDescending(x => x.b.Title)
                .Select(x => new BookViewModel()
                {
                    Id = x.b.Id,
                    FileName = x.b.FileName,
                    Title = x.b.Title,
                    Author = x.b.Author,
                    ThumbnailImage = x.bi.ImagePath
                }).ToListAsync();

            return data;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}