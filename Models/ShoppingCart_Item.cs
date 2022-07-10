using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class ShoppingCart_Item
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public Movie Movie { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
