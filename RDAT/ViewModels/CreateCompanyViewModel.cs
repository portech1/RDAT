using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class CreateCompanyViewModel
    {
        public List<SelectListItem> States { get; set; }

        public Company Company { get; set; }
    }
}
