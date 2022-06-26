 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class Cinema
    { 
        [Key]
        public int Id { get; set; }
        [Display(Name="Cinema Logo")]
        public string Logo { get; set; }
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Display(Name = "Descriptions")]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
