using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Result")]
        public String DisplayName { get; set; }

        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Modified")]
        public DateTime Modified { get; set; }

        public Boolean isDelete { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
