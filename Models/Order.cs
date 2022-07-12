using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser  user { get; set; }
        // Relationship
        public List<OrderItem> orderItems { get; set; }
    }
}
