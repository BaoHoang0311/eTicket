using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Data.LogIn_ViewModel
{
    public class LogInVIewModel
    {
        [Display(Name = "Email của bạn")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Password của bạn")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
