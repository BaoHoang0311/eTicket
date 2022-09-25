using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.LogIn_ViewModel;
using web_movie.Data.Static;
using web_movie.Models;

namespace web_movie.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbcontext _context;

        public AccountController(UserManager<ApplicationUser > userManager,
         SignInManager<ApplicationUser > signInManager, AppDbcontext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        // danh sách các users
        public async Task<IActionResult> Users()
        {
            var list = await _context.Users
                .Include(m=>m.UserRoles).ThenInclude(m=>m.Role)
                .ToListAsync();
            return View(list);
        }
		//zz
        #region Đăng nhập
        public IActionResult LogIn()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Movies");
            }
            return View(new LogInVIewModel());
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVIewModel logInVIewModel)
        {
            if (!ModelState.IsValid) return View(logInVIewModel);
            var user = await _userManager.FindByEmailAsync(logInVIewModel.EmailAddress);
            if (user != null)
            {
                var passcheck = await _userManager.CheckPasswordAsync(user, logInVIewModel.Password);
                if (passcheck == true)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInVIewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Eror"] = " pass sai moi nhap lai";
                return View(logInVIewModel);
            }
            TempData["Eror"] = "tài khoản không tồn tại";
            return View(logInVIewModel);
        }
        #endregion

        #region LogOut
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies", new { });
        }
        #endregion

        #region Đăng ký
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Eror"] = "Tài khoản đã tồn tại";
                return View(registerViewModel);
            }

            ApplicationUser  newuser = new()
            {
				// FullName là custom
                FullName = registerViewModel.FullName,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };
            var newuserpas_response = await _userManager.CreateAsync(newuser, registerViewModel.Password);

            if (newuserpas_response.Succeeded)
            {
				await _userManager.AddToRoleAsync(newuser, Role_User.User);
                return View("RegisterComplete");
            }

            List<IdentityError> errorList = newuserpas_response.Errors.ToList();
            var errors = string.Join(",", errorList.Select(e => e.Description));
            TempData["Eror"] = errors;

            return View(registerViewModel);
        }
        #endregion
		

        public IActionResult AccessDenied()
        {
            return RedirectToAction(nameof(LogIn));
        }
    }
}
