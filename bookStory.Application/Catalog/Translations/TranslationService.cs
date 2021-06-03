using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Catalog.Translations;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Translations
{
    public class TranslationService : ITranslationService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;

        public TranslationService(bookStoryDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<List<TranslationViewModel>> GetAll()
        {
            var query = from b in _context.Translations
                        select b;
            var data = await query.Select(x => new TranslationViewModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                //IdProject = x.IdProject,
                IdParagraph = x.IdParagraph,
                Text = x.Text,
                Rating = x.Rating,
                Date = x.Date
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(TranslationCreateRequest request)
        {
            var item = new Translation()
            {
                UserId = request.UserId,
                //IdProject = request.IdProject,
                IdParagraph = request.IdParagraph,
                Text = request.Text,
                Rating = request.Rating,
                Date = DateTime.Now
            };
            _context.Translations.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Translations.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Translations.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<TranslationViewModel>> GetAllPaging(GetManageTranslationPagingRequest request)
        {
            var query = from t in _context.Translations
                        join u in _userManager.Users on t.UserId equals u.Id
                        join p in _context.Paragraphs on t.IdParagraph equals p.Id into bp
                        from p in bp.DefaultIfEmpty()
                            //join c in _context.Comments on t.Id equals c.IdTranslation into tc
                            //from c in tc.DefaultIfEmpty()
                        select new { t, p, u };

            var querycomment = from c in _context.Comments
                               select c;
            var comments = await (from t in _context.Translations
                                  join c in _context.Comments on t.Id equals c.IdTranslation into tc
                                  from c in tc.DefaultIfEmpty()
                                  select c).ToListAsync();

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.t.Text.Contains(request.Keyword));
            if (request.IdParagraph != null && request.IdParagraph != 0)
            {
                query = query.Where(p => p.t.IdParagraph == request.IdParagraph);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new TranslationViewModel()
                {
                    Id = x.t.Id,
                    UserId = x.t.UserId,
                    //IdProject = x.t.IdProject,
                    IdParagraph = x.t.IdParagraph,
                    Text = x.t.Text,
                    Rating = x.t.Rating,
                    Date = x.t.Date,
                    FirstName = x.u.FirstName,
                    LastName = x.u.LastName
                }).ToListAsync();

            var pagedResult = new PagedResult<TranslationViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<List<CommentViewModel>> ListComment(int idTranslations)
        {
            return await _context.Comments.Where(x => x.IdTranslation == idTranslations)
                .Select(i => new CommentViewModel()
                {
                    Id = i.Id,
                    IdTranslation = i.IdTranslation,
                    UserId = i.UserId,
                    Message = i.Message,
                    DateComment = i.DateComment
                }).ToListAsync();
        }

        public async Task<TranslationViewModel> GetById(int id)
        {
            var item = await _context.Translations.FindAsync(id);
            var bookVM = new TranslationViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                //IdProject = item.IdProject,
                IdParagraph = item.IdParagraph,
                Text = item.Text,
                Rating = item.Rating,
                Date = item.Date
            };
            return bookVM;
        }

        public async Task<int> Update(TranslationUpdateRequest request)
        {
            var item = await _context.Translations.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.UserId = request.UserId;
            //item.IdProject = request.IdProject;
            item.IdParagraph = request.IdParagraph;
            item.Text = request.Text;
            item.Rating = request.Rating;
            item.Date = DateTime.Now;
            return await _context.SaveChangesAsync();
        }
    }
}