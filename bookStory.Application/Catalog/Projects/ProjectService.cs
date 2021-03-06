using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Projects;
using bookStory.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;

        public ProjectService(bookStoryDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<ProjectViewModel>> GetAll()
        {
            var query = from b in _context.Projects
                        select b;
            var data = await query.Select(x => new ProjectViewModel()
            {
                Id = x.Id,
                IdBook = x.IdBook,
                IdLanguage = x.IdLanguage,
                Title = x.Title,
                Description = x.Description,
                UserId = x.UserId,
                Status = x.Status,
                DateProject = x.DateProject
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(ProjectCreateRequest request)
        {
            var item = new Project()
            {
                IdBook = request.IdBook,
                IdLanguage = request.IdLanguage,
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                Status = request.Status,
                DateProject = DateTime.Now
            };
            _context.Projects.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Projects.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Projects.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProjectViewModel>> GetAllPaging(GetManageProjectPagingRequest request)
        {
            var query = from b in _context.Projects
                        join bk in _context.Books on b.IdBook equals bk.Id
                        select new { b, bk };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.b.Title.Contains(request.Keyword));

            if (request.IdBook != null && request.IdBook != 0)
            {
                query = query.Where(p => p.b.IdBook == request.IdBook);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProjectViewModel()
                {
                    Id = x.b.Id,
                    IdBook = x.b.IdBook,
                    IdLanguage = x.b.IdLanguage,
                    Title = x.b.Title,
                    Description = x.b.Description,
                    UserId = x.b.UserId,
                    Status = x.b.Status,
                    DateProject = x.b.DateProject
                }).ToListAsync();

            var pagedResult = new PagedResult<ProjectViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<ProjectViewModel> GetById(int id)
        {
            var item = await _context.Projects.FindAsync(id);
            var bookVM = new ProjectViewModel()
            {
                Id = item.Id,
                IdBook = item.IdBook,
                IdLanguage = item.IdLanguage,
                Title = item.Title,
                Description = item.Description,
                UserId = item.UserId,
                Status = item.Status,
                DateProject = item.DateProject
            };
            return bookVM;
        }

        public async Task<int> Update(ProjectUpdateRequest request)
        {
            var item = await _context.Projects.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.IdBook = request.IdBook;
            item.IdLanguage = request.IdLanguage;
            item.Title = request.Title;
            item.Description = request.Description;
            item.UserId = request.UserId;
            item.Status = request.Status;
            item.DateProject = DateTime.Now;

            return await _context.SaveChangesAsync();
        }
    }
}