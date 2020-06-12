using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class Company {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Address")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Phone No")]
        public string Phone { get; set; }

        [DisplayName("Fax Number")]
        public string Fax { get; set; }

        [DisplayName("DOT No")]
        public string Dot { get; set; }

        [DisplayName("Representative Name")]
        public string RepresentativeName { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("Created")]
        public DateTime? Created { get; set; }

        [DisplayName("Modified")]
        public DateTime? Modified { get; set; }

        public Boolean isDelete { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        // ID From previous application
        public int OldID { get; set; }

        // User has selected this company as a favorite
        public Boolean isFavorite { get; set; }

    }
}
