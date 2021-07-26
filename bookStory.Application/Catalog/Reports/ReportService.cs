using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Reports;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Reports
{
    public class ReportService : IReportService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;

        public ReportService(bookStoryDbContext context, IStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<List<ReportViewModel>> GetAll()
        {
            var query = from b in _context.Reports
                        select b;
            var data = await query.Select(x => new ReportViewModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                IdParagraph = x.IdParagraph,
                Reason = x.Reason
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(ReportCreateRequest request)
        {
            var item = new Report()
            {
                UserId = request.UserId,
                IdParagraph = request.IdParagraph,
                Reason = request.Reason
            };
            _context.Reports.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Reports.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Reports.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ReportViewModel>> GetAllPaging(GetManageReportPagingRequest request)
        {
            var query = from b in _context.Reports
                        join p in _context.Paragraphs on b.IdParagraph equals p.Id
                        join bk in _context.Books on p.IdBook equals bk.Id
                        join u in _userManager.Users on b.UserId equals u.Id
                        select new { b, p, bk, u };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.b.Reason.Contains(request.Keyword));

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ReportViewModel()
                {
                    Id = x.b.Id,
                    UserId = x.b.UserId,
                    IdParagraph = x.b.IdParagraph,
                    Reason = x.b.Reason,
                    TitleBook = x.bk.Title,
                    FirstName = x.u.FirstName,
                    LastName = x.u.LastName
                }).ToListAsync();

            var pagedResult = new PagedResult<ReportViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<ReportViewModel> GetById(int id)
        {
            var item = await _context.Reports.FindAsync(id);
            var pra = await _context.Paragraphs.FindAsync(item.IdParagraph);
            var book = await _context.Books.FindAsync(pra.IdBook);
            var bookVM = new ReportViewModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                IdParagraph = item.IdParagraph,
                Reason = item.Reason,
                TitleBook = book.Title,
                Order = pra.Order
            };
            return bookVM;
        }

        public async Task<int> Update(ReportUpdateRequest request)
        {
            var item = await _context.Reports.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.UserId = request.UserId;
            item.IdParagraph = request.IdParagraph;
            item.Reason = request.Reason;

            return await _context.SaveChangesAsync();
        }
    }
}