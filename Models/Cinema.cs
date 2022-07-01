using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;


namespace web_movie.Models
{
    public class Cinema : IEntityID
    { 
        [Key]
        public int Id { get; set; }
        [Display(Name="Cinema Logo")]
        [Required]
        public string Logo { get; set; }
        [Display(Name ="Name")]
        [Required]
        public string FullName { get; set; }
        [Display(Name = "Descriptions")]
        [Required]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }

        //Relationships -images
        public List<ImageCinemas> images { get; set; }
    }
}
