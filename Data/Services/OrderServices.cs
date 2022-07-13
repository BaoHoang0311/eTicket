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

        public async Task<List<Order>> GetOrdersbyUserIDandRoleID (string RoleID,string userID)
        {
            var orders = await _context.Order
                    .Include(m => m.user)
                    .Include(m => m.orderItems).ThenInclude(m => m.Movie)
                    .ToListAsync();
            if (RoleID != "Admin" )
            {
                orders = orders.Where(m => m.UserID == userID).ToList();
                return orders;
            }
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
