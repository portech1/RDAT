using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class BatchCompanyModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Driver Id")]
        public int Driver_Id { get; set; }

        [DisplayName("Driver Name")]
        public string Driver_Name { get; set; }

        [DisplayName("Company Id")]
        public int Company_Id { get; set; }
        
        [DisplayName("Company Name")]
        public string Company_Name { get; set; }

        [DisplayName("Reported Results")]
        public String Reported_Results { get; set; }

        [DisplayName("Results Date")]
        public DateTime? ResultsDate { get; set; }

        [DisplayName("Batch Id")]
        public int Batch_Id { get; set; }

        [DisplayName("Batch Date")]
        public DateTime? BatchDate { get; set; }

        [DisplayName("Test Type")]
        public string Test_Type { get; set; }

        [DisplayName("Closed Date")]
        public DateTime? ClosedDate { get; set; }

    }
    
    
    public class SingleBatchViewModel
    {
        public List<BatchCompanyModel> TestingLogs { get; set; }
    }
}
