using bookStory.ViewModels.Catalog.Ratings;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Rating
{
    public interface IRatingApiClient
    {
        Task<PagedResult<RatingViewModel>> GetPagings(GetManageRatingPagingRequest request);

        Task<bool> CreateRating(RatingCreateRequest request);

        Task<bool> UpdateRating(RatingUpdateRequest request);

        Task<RatingViewModel> GetById(int id);

        Task<bool> DeleteRating(int id);
    }
}