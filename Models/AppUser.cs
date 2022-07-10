using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class AppUser: IdentityUser
    {
        [Display(Name="FullName")]
        public string FullName { get; set; }
    }
}
