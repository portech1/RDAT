﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class FeaturedDriversViewModel
    {
        public String Title { get; set; }

        public String SearchTerm { get; set; }

        public DateTime Drug_Results_Date { get; set; }

        public DateTime Drug_Test_Date { get; set; }

        public DateTime Alcohol_Results_Date { get; set; }

        public DateTime Alcohol_Test_Date { get; set; }

        public List<DriverSearchResult> Drivers { get; set; }
    }
}
