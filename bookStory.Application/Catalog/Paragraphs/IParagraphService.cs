using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Paragraphs
{
    public interface IParagraphService
    {
        Task<int> Create(ParagraphCreateRequest request);

        Task<int> Update(ParagraphUpdateRequest request);

        Task<int> Delete(int id);

        Task<ParagraphViewModel> GetById(int IdBook);

        Task<PagedResult<ParagraphViewModel>> GetAllPaging(GetManageParagraphPagingRequest request);

        Task<List<ParagraphViewModel>> GetAll();

        Task<List<ParagraphViewModel>> GetAll(int idBook);
    }
}