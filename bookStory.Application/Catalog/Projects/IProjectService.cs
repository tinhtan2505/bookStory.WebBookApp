using bookStory.ViewModels.Catalog.Projects;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Projects
{
    public interface IProjectService
    {
        Task<int> Create(ProjectCreateRequest request);

        Task<int> Update(ProjectUpdateRequest request);

        Task<int> Delete(int id);

        Task<ProjectViewModel> GetById(int IdBook);

        Task<PagedResult<ProjectViewModel>> GetAllPaging(GetManageProjectPagingRequest request);

        Task<List<ProjectViewModel>> GetAll();
    }
}