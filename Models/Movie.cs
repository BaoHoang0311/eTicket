using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;

namespace web_movie.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDay { get; set; }
        public MovieCategory MovieCategory { get; set; }
    }
}
