using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double Price { get; set; }
        public string ImageUrl { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDay { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationship bẳng Actor_Movies
        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        [ForeignKey("CinemaID")]
        public int CinemaID { get; set; }
        public Cinema cinema { get; set; }

        // Producer 
        [ForeignKey("ProducerID")]
        public int ProducerID { get; set; }
        public Producer producer { get; set; }

    }
}
