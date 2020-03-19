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

        public string City { get; set; }

        public string State { get; set; }

        [DisplayName("Phone No")]
        public string Phone { get; set; }

        [DisplayName("Fax Number")]
        public string Fax { get; set; }

        [DisplayName("DOT No")]
        public string Dot { get; set; }

        [DisplayName("Representative Name")]
        public string RepresentativeName { get; set; }

        [DisplayName("Email Id")]
        public string Email { get; set; }


    }
}
