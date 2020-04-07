using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class TestingLog {
        [Key]
        public int Id { get; set; }

        [DisplayName("Batch Id")]
        public int Batch_Id { get; set; }

        [DisplayName("Drug Selection Date")]
        public DateTime? Selectiondatedrug { get; set; }

        [DisplayName("Alcohol Selection Date")]
        public DateTime? Selectiondatealcohol { get; set; }

        [DisplayName("Reported Results")]
        public String Reported_Results { get; set; }

        [DisplayName("Results Date")]
        public DateTime? ResultsDate { get; set; }

        [DisplayName("SSN")]
        public string SSN { get; set; }

        [DisplayName("Specimen Id")]
        public int Specimen_Id { get; set; }

        [DisplayName("Closed Date")]
        public DateTime? ClosedDate { get; set; }

        [DisplayName("Test Process ID")]
        public int Test_Process_Id { get; set; }

        [DisplayName("Driver Id")]
        public int Driver_Id { get; set; }

        [DisplayName("Test Type")]
        public string Test_Type { get; set; }

        [DisplayName("Drug Percentage")]
        public double Drug_Percentage { get; set; }

        [DisplayName("Alcohol Percentage")]
        public double Alcohol_Percentage { get; set; }

        [DisplayName("Created")]
        public DateTime? Created { get; set; }

        [DisplayName("Modified")]
        public DateTime? Modified { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
        
        public Boolean isDelete { get; set; }

    }
}
