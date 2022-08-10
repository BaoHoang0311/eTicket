using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web_movie.Data.Cart;
using web_movie.Data.Services;
using web_movie.Data.Static;
using web_movie.Data.ViewModel;
using web_movie.Models;

namespace web_movie.Controllers
{
    [Authorize(Roles = Role_User.Admin)]
    public class OrderController : Controller
    {
        private readonly IMoviesServices _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderServices _orderServices;

        public OrderController(IMoviesServices movieService
            , ShoppingCart shoppingCart, IOrderServices orderServices
            )
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderServices = orderServices;
        }
        [AllowAnonymous]
        // bấm vào giỏ hàng thấy hàng đang mua, tìm kiếm thông qua giỏ hàng tạm (ShoppingCart_Item)
        public IActionResult Cart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ds_sp = items;

            var res = new ShoppingCart_Model()
            {
                ShoppingCart = _shoppingCart,
                TongTien = _shoppingCart.TongTien()
            };
            return View(res);
        }
        // bấm vào giỏ hàng thấy hàng đã mua
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var ds_hang_da_mua_voi_id_role_cua_minh =  _orderServices.GetOrdersbyUserIDandRoleID(userRole, userId);
            return View(ds_hang_da_mua_voi_id_role_cua_minh);
        }
        [AllowAnonymous]
        // Add to Cart (from Page Movie)
        public async Task<IActionResult> Add(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie != null)
                _shoppingCart.Cong_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
        [AllowAnonymous]
        // Dấu Cộng trong giỏ hàng
        public async Task<IActionResult> Plus_SP(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie != null)
                _shoppingCart.Cong_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
        [AllowAnonymous]
        // Dấu trừ trong giỏ hàng
        public async Task<IActionResult> Tru_SP(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie != null)
                _shoppingCart.Tru_SP(movie);
            return RedirectToAction(nameof(Cart));
        }
        [AllowAnonymous]
        #region hoan tat don hang
        public async Task<IActionResult> Hoantat()
        {
            var dssp = _shoppingCart.GetShoppingCartItems();
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);
            await _orderServices.StoreOrder(dssp, userID, email);
            await _shoppingCart.DeleteGioHangTam();

            // localhost/Order/Hoantat
            return View("ThankYou");
        }
        #endregion
    }
}
