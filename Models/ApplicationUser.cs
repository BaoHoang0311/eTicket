using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="FullName")]
        public string FullName { get; set; }

        //public List<ApplicationUserRole> UserRoles { get; set; }

    }
}
