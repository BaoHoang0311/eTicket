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
        public string Logo { get; set; }
        [Display(Name ="Name")]
        public string FullName { get; set; }
        [Display(Name = "Descriptions")]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
