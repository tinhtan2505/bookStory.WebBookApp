using bookStory.ViewModels.Catalog.Translations;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Translation
{
    public interface ITranslationApiClient
    {
        Task<PagedResult<TranslationViewModel>> GetPagings(GetManageTranslationPagingRequest request);

        Task<List<TranslationViewModel>> GetAll();

        Task<bool> CreateTranslation(TranslationCreateRequest request);

        Task<bool> UpdateTranslation(TranslationUpdateRequest request);

        Task<TranslationViewModel> GetById(int id);

        Task<bool> DeleteTranslation(int id);
    }
}