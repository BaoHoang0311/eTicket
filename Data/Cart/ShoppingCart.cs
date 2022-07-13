using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbcontext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCart_Item> ds_sp { get; set; }
        public ShoppingCart(AppDbcontext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service
                .GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            var context = service.GetService<AppDbcontext>();
            
            string cartID = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartID);

            return new ShoppingCart(context) { ShoppingCartId = cartID };
        }
        // giỏ hàng tạm thời
        // bấm vào giỏ hàng thấy hàng mua tìm thông qua thằng ShoppingCartId==session
        public List<ShoppingCart_Item> GetShoppingCartItems()
        {
            ds_sp = _context.ShoppingCart_Items
                    .Where(n => n.ShoppingCartId == ShoppingCartId)
                    .Include(m => m.Movie).ToList();
            return ds_sp;
        }
        public void Cong_SP(Movie movie)
        {
            ShoppingCart_Item shoppingCartItem = _context.ShoppingCart_Items
                .FirstOrDefault(n => n.Movie.Id == movie.Id
                && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCart_Item()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCart_Items.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void Tru_SP(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCart_Items
                   .FirstOrDefault(n => n.Movie.Id == movie.Id
                    && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCart_Items.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }
        public async Task DeleteGioHangTam()
        {
            // tìm sp tạm thời trong ShoppingcartID(session) để xóa
            var dssp = await _context.ShoppingCart_Items
                .Where(x => x.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCart_Items.RemoveRange(dssp);
            await _context.SaveChangesAsync();
        }
        public double TongTien()
        {
            var total = _context.ShoppingCart_Items
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(x => x.Movie.Price * x.Amount).Sum();
            return total;
        }
    }
}
