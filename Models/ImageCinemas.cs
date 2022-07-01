using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class ImageCinemas
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public int CinemaID { get; set; }
        [ForeignKey("CinemaID")]
        public Cinema cinema { get; set; }
    }
}
