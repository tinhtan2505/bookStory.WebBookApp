using bookStory.ViewModels.Catalog.Projects;
using bookStory.ViewModels.Catalog.Reports;
using bookStory.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStory.AdminApp.Models
{
    public class HomeViewModel : PagingRequestBase
    {
        public PagedResult<ProjectViewModel> ListProjects { get; set; }
        public PagedResult<ReportViewModel> ListReports { get; set; }
    }
}