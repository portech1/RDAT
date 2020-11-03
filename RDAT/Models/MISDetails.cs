using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class MISDetails
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Random Test Selection")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Random_Test_Selection_Date { get; set; }

        [DisplayName("Batch Number")]
        public int Batch_Number { get; set; }

        [DisplayName("Driver")]
        public string Driver_Name { get; set; }

        [DisplayName("Company")]
        public string Company_Name { get; set; }

        [DisplayName("Test Type")]
        public string Test_Type { get; set; }

        [DisplayName("Result")]
        public string Result { get; set; }

        [DisplayName("Results Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ResultsDate { get; set; }
    }
}
