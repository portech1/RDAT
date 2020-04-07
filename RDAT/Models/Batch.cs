using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class Batch
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Modified")]
        public DateTime Modified { get; set; }

        [DisplayName("Run Date")]
        public DateTime RunDate { get; set; }

        public int User_Id { get; set; }

        [DisplayName("Eligible Drivers")]
        public int Eligible_Drivers { get; set; }

        [DisplayName("Eligible Drivers")]
        public int Drug_Tests { get; set; }

        [DisplayName("Eligible Drivers")]
        public int Alcohol_Tests { get; set; }

        [DisplayName("Eligible Drivers")]
        public int Drug_Percentage { get; set; }

        [DisplayName("Eligible Drivers")]
        public int Alcohol_Percentage { get; set; }

        [DisplayName("Replaced By")]
        public int Replaced_By { get; set; }

        public Boolean isDelete { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
