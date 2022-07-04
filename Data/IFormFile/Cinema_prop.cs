using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.IFormFile
{
    public class Cinema_prop : Cinema
    {
        //Relationships - images
        [Display(Name = "Choose the gallery images of Cinema")]
        [Required]
        public IFormFileCollection Gallery_FormFiles { get; set; }
        public List<ImageCinemas> Gallery { get; set; }
    }
}
