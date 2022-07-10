using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public interface IOrderServices
    {
        Task StoreOrder(List<ShoppingCart_Item> item, string userID, string email);
        Task<List<Order>> GetOrdersbyUserID(string userID="");
        
    }
}
