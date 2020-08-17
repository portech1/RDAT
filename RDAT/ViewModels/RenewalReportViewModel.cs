using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class RenewalReportViewModel
    {
        public int RenewalMonth { get; set; }

        public List<DriverCompanyModel> Drivers { get; set; }

    }

}
