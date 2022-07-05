using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using web_movie.Models;

namespace web_movie.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbcontext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ds_sp { get; set; }
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
        public void Them_SP(Movie movie)
        {
            ShoppingCartItem shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id
                && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void Tru_SP(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
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
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            ds_sp = _context.ShoppingCartItems
                    .Where(n => n.ShoppingCartId == ShoppingCartId)
                    .Include(m => m.Movie).ToList();
            return ds_sp;
        }
        public double TongTien()
        {
            var total = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(x => x.Movie.Price * x.Amount).Sum();
            return total;
        }
    }
}
