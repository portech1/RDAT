using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class Invoice {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DriverId { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }


        [DisplayName("Paid Date")]
        public DateTime PaidDate { get; set; }

        // driver has paid invoice
        public Boolean isPaid { get; set; }

    }
}
