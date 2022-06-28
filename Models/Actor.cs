using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture URL")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Bioraphy")]
        public string Bio { get; set; }
        //Relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
