using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class OrderServices : IOrderServices
    {
        public AppDbcontext _context;
        public OrderServices(AppDbcontext context)
        {
            _context = context;
        }

        public List<IGrouping<(string, string, string), (string,string,double,int)>> GetOrdersbyUserIDandRoleID(string RoleID, string userID)
        {

            var sp = from or in _context.Order
                     join or_item in _context.OrderItem on or.Id equals or_item.OrderID
                     join movie in _context.Movies on or_item.MovieID equals movie.Id
                     join b in _context.UserRoles on or.UserID equals b.UserId
                     join name in _context.Users on b.UserId equals name.Id
                     select new
                     {
                         or_id = or.Id.ToString(),
                         user_id = or.UserID,
                         name =name.UserName,
                         email = or.Email,
                         role = b.Role.Name,
                         movie_name = movie.FullName,
                         movie_price =movie.Price,
                         so_luong = or_item.so_luong
                     };

            var orders = new List<IGrouping<( string, string, string), (string,string, double,int)>>();

            if (RoleID != "Admin")
            {
                var orders12 = (from s in sp.AsEnumerable()
                                where s.user_id == userID
                                group (s.or_id, s.movie_name,s.movie_price,s.so_luong) by (s.name, s.email, s.role)).ToList();
                return orders12;
            }
            orders = (from s in sp.AsEnumerable()
                      group (s.or_id, s.movie_name, s.movie_price, s.so_luong) by (s.name, s.email, s.role)).ToList();
            return orders;
        }
        public async Task StoreOrder(List<ShoppingCart_Item> item, string userID, string email)
        {
            Order order = new Order()
            {
                Email = email,
                UserID = userID,
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var sp in item)
            {
                OrderItem orderitem = new OrderItem()
                {
                    OrderID = order.Id,
                    so_luong = sp.Amount,
                    MovieID = sp.Movie.Id,
                    Price = sp.Movie.Price
                };
                await _context.OrderItem.AddAsync(orderitem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
