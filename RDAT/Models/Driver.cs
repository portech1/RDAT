using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class Driver {
        [Key]
        public int Id { get; set; }

        public Guid UniqueDriverId { get; set; }

        [Required]
        public string DriverName { get; set; }

        [DisplayName("SSN")]
        public string SSN { get; set; }

        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Zipcode")]
        public string Zipcode { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("UniqueId")]
        public Guid UniqueId { get; set; }

        [DisplayName("Phone No")]
        public string Phone { get; set; }

        [DisplayName("Fax No")]
        public string Fax { get; set; }

        [DisplayName("Cell No")]
        public string Cell { get; set; }

        [DisplayName("Email Id")]
        public string Email { get; set; }

        [DisplayName("Enrollment Date")]
        public DateTime? EnrollmentDate { get; set; }

        [DisplayName("TerminationDate")]
        public DateTime? TerminationDate { get; set; }

        [DisplayName("CDL")]
        public string CDL { get; set; }

        public int Company_id { get; set; }

        public int OldCompanyId { get; set; }

        public int OldRDAT1Id { get; set; }


        // User has selected this driver as a favorite
        public Boolean isFavorite { get; set; }

        public Boolean isDelete { get; set; }

    }
}
