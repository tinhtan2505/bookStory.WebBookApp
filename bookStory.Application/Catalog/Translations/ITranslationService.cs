using bookStory.ViewModels.Catalog.Translations;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Translations
{
    public interface ITranslationService
    {
        Task<int> Create(TranslationCreateRequest request);

        Task<int> Update(TranslationUpdateRequest request);

        Task<int> Delete(int id);

        Task<TranslationViewModel> GetById(int IdBook);

        Task<PagedResult<TranslationViewModel>> GetAllPaging(GetManageTranslationPagingRequest request);

        Task<List<TranslationViewModel>> GetAll();
    }
}