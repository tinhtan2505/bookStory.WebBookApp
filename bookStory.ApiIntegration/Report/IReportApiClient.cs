using bookStory.ViewModels.Catalog.Reports;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ApiIntegration.Report
{
    public interface IReportApiClient
    {
        Task<PagedResult<ReportViewModel>> GetPagings(GetManageReportPagingRequest request);

        Task<bool> CreateReport(ReportCreateRequest request);

        Task<bool> UpdateReport(ReportUpdateRequest request);

        Task<ReportViewModel> GetById(int id);

        Task<bool> DeleteReport(int id);
    }
}