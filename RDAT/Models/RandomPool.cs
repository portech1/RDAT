using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    
    public class RandomPool
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Random Test Selection")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Random_Test_Selection_Date { get; set; }

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

        [DisplayName("Negative Tested Drivers")]
        public int Negative_Tested_Drivers { get; set; }

        [DisplayName("Selection Test Ratio")]
        
        public double Selection_Test_Ratio { get; set; }

        [DisplayName("Annual Ratio")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public double Annual_Ratio { get; set; }
    }
}
