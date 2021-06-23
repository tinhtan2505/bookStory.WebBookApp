using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Ratings;
using bookStory.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;

        public RatingService(bookStoryDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<RatingViewModel>> GetAll()
        {
            var query = from b in _context.Ratings
                        select b;
            var data = await query.Select(x => new RatingViewModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                IdTranslation = x.IdTranslation,
                Vote = x.Vote
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(RatingCreateRequest request)
        {
            var item = new Rating()
            {
                UserId = request.UserId,
                IdTranslation = request.IdTranslation,
                Vote = request.Vote
            };
            _context.Ratings.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Ratings.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Ratings.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<RatingViewModel>> GetAllPaging(GetManageRatingPagingRequest request)
        {
            var query = from b in _context.Ratings
                        select b;
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.UserId.ToString().Contains(request.Keyword));

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new RatingViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    IdTranslation = x.IdTranslation,
                    Vote = x.Vote
                }).ToListAsync();

            var pagedResult = new PagedResult<RatingViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<RatingViewModel> GetRating(Guid keywordUserId, int keywordIdTranslation)
        {
            //var bookVM = new RatingViewModel();
            var query = from b in _context.Ratings
                        where b.UserId == keywordUserId && b.IdTranslation == keywordIdTranslation
                        select b;

            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new RatingViewModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                IdTranslation = x.IdTranslation,
                Vote = x.Vote
            }).ToListAsync();
            var item = data.FirstOrDefault();
            if (item != null)
            {
                var bookVM = new RatingViewModel()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    IdTranslation = item.IdTranslation,
                    Vote = item.Vote
                };
                return bookVM;
            }
            else return null;
        }

        public async Task<RatingViewModel> GetById(int id)
        {
            var item = await _context.Ratings.FindAsync(id);
            var bookVM = new RatingViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                IdTranslation = item.IdTranslation,
                Vote = item.Vote
            };
            return bookVM;
        }

        public async Task<int> Update(RatingUpdateRequest request)
        {
            var item = await _context.Ratings.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.UserId = request.UserId;
            item.IdTranslation = request.IdTranslation;
            item.Vote = request.Vote;

            return await _context.SaveChangesAsync();
        }
    }
}