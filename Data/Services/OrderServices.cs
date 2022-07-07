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
        public async Task<List<Order>> GetOrdersbyUserID(string userID)
        {
            var orders = new List<Order>();
            if (userID == "")
            {
                orders = await _context.Order.Include(m => m.orderItems).ThenInclude(m => m.Movie)
                .Where(m => m.UserID == userID).ToListAsync();

                return orders;
            }

            orders = await _context.Order.Include(m => m.orderItems)
                .ThenInclude(m => m.Movie)
                .Where(m=>m.UserID==userID)
                .ToListAsync();

            return orders;
        }

        public async Task StoreOrder(List<ShoppingCartItem> item, string userID, string email)
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
                    OrderID=order.Id,
                    so_luong=sp.Amount,
                    MovieID=sp.Movie.Id,
                    Price=sp.Movie.Price
                };
                await _context.OrderItem.AddAsync(orderitem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
