using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class MISReportViewModel
    {
        public MISRequest ReportRequest { get; set; }

        public List<SelectListItem> Companies { get; set; }

        public List<SummaryTestType> SummaryTestTypes { get; set; }

        public List<SummaryTestType> Totals { get; set; }

    }

    public class MISRequest
    {
        public int CompanyID { get; set; }

        public string TestType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IncludeDriverDetails { get; set; }
    }
}
