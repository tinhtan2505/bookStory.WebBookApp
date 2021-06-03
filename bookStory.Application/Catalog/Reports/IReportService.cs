using bookStory.ViewModels.Catalog.Reports;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Application.Catalog.Reports
{
    public interface IReportService
    {
        Task<int> Create(ReportCreateRequest request);

        Task<int> Update(ReportUpdateRequest request);

        Task<int> Delete(int id);

        Task<ReportViewModel> GetById(int IdBook);

        Task<PagedResult<ReportViewModel>> GetAllPaging(GetManageReportPagingRequest request);

        Task<List<ReportViewModel>> GetAll();
    }
}