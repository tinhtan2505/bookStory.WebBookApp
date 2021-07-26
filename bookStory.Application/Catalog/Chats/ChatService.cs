using bookStory.Application.Catalog.Comments;
using bookStory.Application.Common;
using bookStory.Data.EF;
using bookStory.Data.Entities;
using bookStory.Utilities.Exceptions;
using bookStory.ViewModels.Catalog.Chats;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Chats
{
    public class ChatService : IChatService
    {
        private readonly bookStoryDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;

        public ChatService(bookStoryDbContext context, IStorageService storageService, UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<List<ChatViewModel>> GetAll()
        {
            var query = from c in _context.Chats
                        join u in _userManager.Users on c.UserIdSender equals u.Id
                        join u1 in _userManager.Users on c.UserIdReceiver equals u1.Id
                        select new { c, u, u1 };
            var data = await query.OrderBy(x => x.c.DateComment).Select(x => new ChatViewModel()
            {
                Id = x.c.Id,
                UserIdSender = x.c.UserIdSender,
                UserIdReceiver = x.c.UserIdReceiver,
                Message = x.c.Message,
                DateComment = x.c.DateComment,
                FirstName1 = x.u.FirstName,
                LastName1 = x.u.LastName,
                UserName1 = x.u.UserName,
                FirstName2 = x.u1.FirstName,
                LastName2 = x.u1.LastName,
                UserName2 = x.u1.UserName
            }).ToListAsync();
            return data;
        }

        public async Task<int> Create(ChatCreateRequest request)
        {
            var item = new Chat()
            {
                UserIdSender = request.UserIdSender,
                UserIdReceiver = request.UserIdReceiver,
                Message = request.Message,
                DateComment = DateTime.Now
            };
            _context.Chats.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> Delete(int id)
        {
            var item = await _context.Chats.FindAsync(id);
            if (item == null) throw new BookException($"Khong the tim thay product: {id}");

            _context.Chats.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ChatViewModel>> GetAllPaging(GetManageChatPagingRequest request)
        {
            var query = from c in _context.Chats
                        join u in _userManager.Users on c.UserIdSender equals u.Id
                        join u1 in _userManager.Users on c.UserIdReceiver equals u1.Id
                        select new { c, u, u1 };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Message.Contains(request.Keyword));
            //if (request.Message != null)
            //{
            //    query = query.Where(p => p.c.IdTranslation == request.IdTranslation);
            //}

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ChatViewModel()
                {
                    Id = x.c.Id,
                    UserIdSender = x.c.UserIdSender,
                    UserIdReceiver = x.c.UserIdReceiver,
                    Message = x.c.Message,
                    DateComment = x.c.DateComment,
                    FirstName1 = x.u.FirstName,
                    LastName1 = x.u.LastName,
                    UserName1 = x.u.UserName,
                    FirstName2 = x.u1.FirstName,
                    LastName2 = x.u1.LastName,
                    UserName2 = x.u1.UserName
                }).ToListAsync();

            var pagedResult = new PagedResult<ChatViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data,
            };
            return pagedResult;
        }

        public async Task<ChatViewModel> GetById(int id)
        {
            var item = await _context.Chats.FindAsync(id);
            var bookVM = new ChatViewModel()
            {
                Id = item.Id,
                UserIdSender = item.UserIdSender,
                UserIdReceiver = item.UserIdReceiver,
                Message = item.Message,
                DateComment = item.DateComment
            };
            return bookVM;
        }

        public async Task<int> Update(ChatUpdateRequest request)
        {
            var item = await _context.Chats.FindAsync(request.Id);

            if (item == null) throw new BookException($"Cannot find a product with id: {request.Id}");

            item.UserIdSender = request.UserIdSender;
            item.UserIdReceiver = request.UserIdReceiver;
            item.Message = request.Message;
            item.DateComment = DateTime.Now;

            return await _context.SaveChangesAsync();
        }
    }
}