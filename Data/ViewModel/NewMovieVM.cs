using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using web_movie.Models;

namespace web_movie.Data.ViewModel
{
    public class NewMovieVM : Movie
    {
        [Display(Name = "Chọn diễn viên (multi-select)")]
        [Required(ErrorMessage = "Chọn Ds diễn viên")]
        public List<int> Ds_actor { get; set; }
    }
}
