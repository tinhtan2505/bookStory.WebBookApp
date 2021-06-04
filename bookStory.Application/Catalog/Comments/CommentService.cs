using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Comments
{
    public class CommentService : ICommentService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;

        public CommentService(bookStoryDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<List<CommentViewModel>> GetAll()
        {
            var query = from c in _context.Comments
                        join u in _userManager.Users on c.UserId equals u.Id
                        select new { c, u };
            var data = await query.OrderBy(x => x.c.DateComment).Select(x => new CommentViewModel()
            {
                Id = x.c.Id,
                UserId = x.c.UserId,
                IdTranslation = x.c.IdTranslation,
                Message = x.c.Message,
                DateComment = x.c.DateComment,
                FirstName = x.u.FirstName,
                LastName = x.u.LastName
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(CommentCreateRequest request)
        {
            var item = new Comment()
            {
                UserId = request.UserId,
                IdTranslation = request.IdTranslation,
                Message = request.Message,
                DateComment = DateTime.Now
            };
            _context.Comments.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Comments.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Comments.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<CommentViewModel>> GetAllPaging(GetManageCommentPagingRequest request)
        {
            var query = from c in _context.Comments
                        join t in _context.Translations on c.IdTranslation equals t.Id into ct
                        from t in ct.DefaultIfEmpty()
                        select new { c, t };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Message.Contains(request.Keyword));
            if (request.IdTranslation != null && request.IdTranslation != 0)
            {
                query = query.Where(p => p.c.IdTranslation == request.IdTranslation);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new CommentViewModel()
                {
                    Id = x.c.Id,
                    UserId = x.c.UserId,
                    IdTranslation = x.c.IdTranslation,
                    Message = x.c.Message,
                    DateComment = x.c.DateComment
                }).ToListAsync();

            var pagedResult = new PagedResult<CommentViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<CommentViewModel> GetById(int id)
        {
            var item = await _context.Comments.FindAsync(id);
            var bookVM = new CommentViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                IdTranslation = item.IdTranslation,
                Message = item.Message,
                DateComment = item.DateComment
            };
            return bookVM;
        }

        public async Task<int> Update(CommentUpdateRequest request)
        {
            var item = await _context.Comments.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.UserId = request.UserId;
            item.IdTranslation = request.IdTranslation;
            item.Message = request.Message;
            item.DateComment = DateTime.Now;

            return await _context.SaveChangesAsync();
        }
    }
}