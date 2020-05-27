using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    
    public class RandomPoolDetails
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

        [DisplayName("Company")]
        public string Company { get; set; }

        [DisplayName("Selected Driver")]
        public string Selected_Driver { get; set; }

        [DisplayName("Excused Driver")]
        public string Excused_Driver { get; set; }

        [DisplayName("Positive Tested Driver")]
        public string Positive_Tested_Driver { get; set; }

        [DisplayName("Selection Test Ratio")]
        public double Selection_Test_Ratio { get; set; }

        [DisplayName("Annual Ratio")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public double Annual_Ratio { get; set; }
    }
}
