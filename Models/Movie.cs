﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Base;

namespace web_movie.Models
{
    public class Movie : IEntityID
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDay { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relationship bẳng Actor_Movies
        public List<Actor_Movie> Actors_Movies { get; set; }
        public List<OrderItem> orderItems { get; set; }

        // Cinema
        public int CinemaID { get; set; }
        [ForeignKey("CinemaID")]
        public Cinema cinema { get; set; }
        
        // Producer 
        public int ProducerID { get; set; }
        [ForeignKey("ProducerID")]
        public Producer producer { get; set; }


    }
}
