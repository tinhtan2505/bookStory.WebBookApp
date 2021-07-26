using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Catalog.Translations;
using bookStory.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Paragraphs
{
    public class ParagraphService : IParagraphService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;

        public ParagraphService(bookStoryDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<ParagraphViewModel>> GetAll()
        {
            var query = from b in _context.Paragraphs
                        select b;
            var data = await query.Select(x => new ParagraphViewModel()
            {
                Id = x.Id,
                IdBook = x.IdBook,
                Order = x.Order,
                Type = x.Type
            }).ToListAsync();
            return data;
        }

        public async Task<List<ParagraphViewModel>> GetAll(int idBook)
        {
            var query = from p in _context.Paragraphs
                        where p.IdBook == idBook
                        select p;
            var data = await query.Select(x => new ParagraphViewModel()
            {
                Id = x.Id,
                IdBook = x.IdBook,
                Order = x.Order,
                Type = x.Type
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(ParagraphCreateRequest request)
        {
            var item = new Paragraph()
            {
                IdBook = request.IdBook,
                Order = request.Order,
                Type = request.Type
            };
            _context.Paragraphs.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Paragraphs.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Paragraphs.Remove(item);
            return await _context.SaveChangesAsync();
        }

        //public async Task<TranslationViewModel> GetTranslationMax(int idParagraph)
        //{
        //    var result = _context.Translations.First(x => x.IdParagraph == idParagraph && x.Rating == _context.Translations.Where(y => y.IdParagraph == idParagraph).Max(y => y.Rating));

        //    var bookVM = new TranslationViewModel()
        //    {
        //        Id = result.Id,
        //        UserId = result.UserId,
        //        IdParagraph = result.IdParagraph,
        //        Text = result.Text,
        //        Rating = result.Rating,
        //        Date = result.Date
        //    };
        //    return bookVM;
        //}

        public async Task<PagedResult<ParagraphViewModel>> GetAllPaging(GetManageParagraphPagingRequest request)
        {
            //TranslationViewModel tran = await GetTranslationMax(4);
            var query = from p in _context.Paragraphs
                        join b in _context.Books on p.IdBook equals b.Id into bp
                        from b in bp.DefaultIfEmpty()
                        select new { p, b };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Order.Contains(request.Keyword));
            if (request.IdBook != null && request.IdBook != 0)
            {
                query = query.Where(p => p.p.IdBook == request.IdBook);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ParagraphViewModel()
                {
                    Id = x.p.Id,
                    IdBook = x.p.IdBook,
                    Order = x.p.Order,
                    Type = x.p.Type,
                    RatingMax = _context.Translations.First(z => z.IdParagraph == x.p.Id && z.Rating == _context.Translations.Where(y => y.IdParagraph == x.p.Id).Max(y => y.Rating)).Text
                }).ToListAsync();

            var pagedResult = new PagedResult<ParagraphViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<ParagraphViewModel> GetById(int id)
        {
            var item = await _context.Paragraphs.FindAsync(id);
            var book = await _context.Books.FindAsync(item.IdBook);
            var bookVM = new ParagraphViewModel()
            {
                Id = item.Id,
                IdBook = item.IdBook,
                Order = item.Order,
                Type = item.Type,
                TitleBook = book.Title
            };
            return bookVM;
        }

        public async Task<int> Update(ParagraphUpdateRequest request)
        {
            var item = await _context.Paragraphs.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.IdBook = request.IdBook;
            item.Order = request.Order;
            item.Type = request.Type;

            return await _context.SaveChangesAsync();
        }
    }
}