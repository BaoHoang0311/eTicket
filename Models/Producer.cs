﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;

namespace web_movie.Models
{
    public class Producer:IEntityID
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Producer")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        [StringLength(maximumLength:100, MinimumLength =3)]
        public string FullName { get; set; }
        [Display(Name = "Bio")]
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Bio { get; set; }

        // Relationship
        public List<Movie> Movies { get; set; }
    }
}
