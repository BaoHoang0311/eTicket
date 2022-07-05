using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Cart;
using web_movie.Data.Services;
using web_movie.Data.ViewModel;

namespace web_movie.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMoviesServices _movieService;
        private readonly ShoppingCart _shoppingCart;
        public OrderController(IMoviesServices movieService
            , ShoppingCart shoppingCart)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Cart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ds_sp = items;

            var response = new ShoppingCart_Model()
            {
                ShoppingCart = _shoppingCart,
                TongTien=_shoppingCart.TongTien()
            };
            return View(response);
        }
        
        public async Task<IActionResult> Add(int id )
        {
            var movie = await _movieService.GetById(id);
            if(movie!= null)
                _shoppingCart.Them_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
        public async Task<IActionResult> Plus_SP(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie != null)
                _shoppingCart.Them_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
        public async Task<IActionResult> Tru_SP(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie != null)
                _shoppingCart.Tru_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
    }
}
