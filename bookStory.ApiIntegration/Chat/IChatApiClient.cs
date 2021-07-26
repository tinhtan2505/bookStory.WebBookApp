using bookStory.ViewModels.Catalog.Chats;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Chat
{
    public interface IChatApiClient
    {
        Task<PagedResult<ChatViewModel>> GetPagings(GetManageChatPagingRequest request);

        Task<List<ChatViewModel>> GetAll();

        Task<bool> CreateChat(ChatCreateRequest request);

        Task<bool> UpdateChat(ChatUpdateRequest request);

        Task<ChatViewModel> GetById(int id);

        Task<bool> DeleteChat(int id);
    }
}