using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class User : IdentityUser
     {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        [DisplayName("Email Id")]
        public string Email { get; set; }


    }
}
