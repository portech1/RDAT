using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class DriverSearchModel
    {
        
            [Display(Name = "Driver Name")]
            public String DriverName { get; set; }

            [Display(Name = "Company")]
            public String Company { get; set; }

            [Display(Name = "CDL")]
            public String CDL { get; set; }

            public Int32 Page { get; set; }
            public Int32 PageSize { get; set; }
            public String Sort { get; set; }
            public String SortDir { get; set; }
            public Int32 TotalRecords { get; set; }
            public List<DriverCompanyModel> Drivers { get; set; }

            public DriverSearchModel()
            {
                Page = 1;
                PageSize = 5;
                Sort = "DriverName";
                SortDir = "DESC";
            }
        
    }
}
