using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string Email { get; set; } // example, not necessary
    }
}
