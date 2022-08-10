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
        List<IGrouping<(string, string, string), (string,string, double, int)>> GetOrdersbyUserIDandRoleID(string RoleID,string userID = "" );
    }
}
