using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class SummaryTestType
    {
        public string Description { get; set; }

        [DisplayName("Random Test Selection")]
        //public DataSetDateTime Random_Test_Selection { get; set; }
        public DateTime Random_Test_Selection { get; set; }

        [DisplayName("Batch Number")]
        public int Batch_Number { get; set; }

        [DisplayName("Active Enrolled Drivers")]
        public int Active_Enrolled_Drivers { get; set; }

        [DisplayName("Selected Drivers")]
        public int Selected_Drivers { get; set; }

        [DisplayName("Excused Drivers")]
        public int Excused_Drivers { get; set; }

        [DisplayName("Positive Tested Drivers")]
        public int Positive_Tested_Drivers { get; set; }

        [DisplayName("SSN")]
        public int Negative_Tested_Drivers { get; set; }

        [DisplayName("Selection Test Ratio")]
        public double Selection_Test_Ratio { get; set; }

        [DisplayName("Annual Ratio")]
        public double Annual_Ratio { get; set; }

    }
}
