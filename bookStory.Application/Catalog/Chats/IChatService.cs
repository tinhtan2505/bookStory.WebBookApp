using bookStory.ViewModels.Catalog.Chats;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Chats
{
    public interface IChatService
    {
        Task<int> Create(ChatCreateRequest request);

        Task<int> Update(ChatUpdateRequest request);

        Task<int> Delete(int id);

        Task<ChatViewModel> GetById(int IdBook);

        Task<PagedResult<ChatViewModel>> GetAllPaging(GetManageChatPagingRequest request);

        Task<List<ChatViewModel>> GetAll();
    }
}