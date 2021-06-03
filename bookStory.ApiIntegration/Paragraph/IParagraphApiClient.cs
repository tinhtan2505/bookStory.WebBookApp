using bookStory.ViewModels.Catalog.Paragraps;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Paragraph
{
    public interface IParagraphApiClient
    {
        Task<PagedResult<ParagraphViewModel>> GetPagings(GetManageParagraphPagingRequest request);

        Task<List<ParagraphViewModel>> GetAll();

        Task<bool> CreateParagraph(ParagraphCreateRequest request);

        Task<bool> UpdateParagraph(ParagraphUpdateRequest request);

        Task<ParagraphViewModel> GetById(int id);

        Task<bool> DeleteParagraph(int id);
    }
}