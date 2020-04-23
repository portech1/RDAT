using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class TempTestingLog {
        [Key]
        public int Id { get; set; }

        [DisplayName("SSN")]
        public string SSN { get; set; }

        [DisplayName("Driver Id")]
        public int Driver_Id { get; set; }

        [DisplayName("Company Id")]
        public int Company_Id { get; set; }

        [DisplayName("Driver Name")]
        public string Driver_Name { get; set; }

        [DisplayName("Test Type")]
        public string Test_Type { get; set; }

        [DisplayName("Closed Date")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Closed Date")]
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("IsDelete")]
        public bool is_delete { get; set; }


    }
}
