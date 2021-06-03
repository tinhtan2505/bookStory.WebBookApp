using bookStory.ViewModels.Catalog.Projects;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Project
{
    public interface IProjectApiClient
    {
        Task<PagedResult<ProjectViewModel>> GetPagings(GetManageProjectPagingRequest request);

        Task<bool> CreateProject(ProjectCreateRequest request);

        Task<bool> UpdateProject(ProjectUpdateRequest request);

        Task<ProjectViewModel> GetById(int id);

        Task<bool> DeleteProject(int id);
    }
}