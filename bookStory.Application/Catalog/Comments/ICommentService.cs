using bookStory.ViewModels.Catalog.Chats;
using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Comments
{
    public interface ICommentService
    {
        Task<int> Create(CommentCreateRequest request);

        Task<int> Update(CommentUpdateRequest request);

        Task<int> Delete(int id);

        Task<CommentViewModel> GetById(int IdBook);

        Task<PagedResult<CommentViewModel>> GetAllPaging(GetManageCommentPagingRequest request);

        Task<List<CommentViewModel>> GetAll();
    }
}