using bookStory.ViewModels.Catalog.Comments;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Comment
{
    public interface ICommentApiClient
    {
        Task<PagedResult<CommentViewModel>> GetPagings(GetManageCommentPagingRequest request);

        Task<List<CommentViewModel>> GetAll();

        Task<bool> CreateComment(CommentCreateRequest request);

        Task<bool> UpdateComment(CommentUpdateRequest request);

        Task<CommentViewModel> GetById(int id);

        Task<bool> DeleteComment(int id);
    }
}