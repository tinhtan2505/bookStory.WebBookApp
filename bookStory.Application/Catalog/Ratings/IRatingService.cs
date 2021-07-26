using bookStory.ViewModels.Catalog.Ratings;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Ratings
{
    public interface IRatingService
    {
        Task<int> Create(RatingCreateRequest request);

        Task<int> Update(RatingUpdateRequest request);

        Task<int> Delete(int id);

        Task<RatingViewModel> GetById(int IdBook);

        Task<RatingViewModel> GetRating(Guid keywordUserId, int keywordIdTranslation);

        Task<PagedResult<RatingViewModel>> GetAllPaging(GetManageRatingPagingRequest request);

        Task<List<RatingViewModel>> GetAll();
    }
}