using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;

namespace web_movie.Models
{
    public class Actor : IEntityID
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture URL")]
        [Required(ErrorMessage ="Yêu cầu nhập hình ảnh")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage ="chuỗi phải có độ dài 3<= và <50")]
        public string FullName { get; set; }
        [Display(Name = "Bioraphy")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Bio { get; set; }

        //Relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
