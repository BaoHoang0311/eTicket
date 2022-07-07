using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int so_luong { get; set; }
        public double Price { get; set; }


        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public  Movie Movie { get; set; }

        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order order { get; set; }
    }
}
