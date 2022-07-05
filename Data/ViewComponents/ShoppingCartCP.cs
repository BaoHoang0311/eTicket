using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Cart;

namespace web_movie.Data.ViewComponents
{
    public class ShoppingCartCP : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartCP(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        // số ở giỏ hàng
        public IViewComponentResult Invoke()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            return View(item.Count);
        }
    }
}
