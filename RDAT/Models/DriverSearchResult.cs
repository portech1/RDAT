using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class DriverSearchResult {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DriverName { get; set; }

        public int Company_id { get; set; }

        public string CompanyName { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        // User has selected this driver as a favorite
        public Boolean isFavorite { get; set; }

        // User is in the latest batch
        public Boolean isLatestBatch { get; set; }

    }
}
