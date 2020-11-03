using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class CreateDriverViewModel
    {
        public List<SelectListItem> Companies { get; set; }

        public List<SelectListItem> States { get; set; }

        public bool IsReadOnly { get; set; }

        public Driver Driver { get; set; }
    }
}
