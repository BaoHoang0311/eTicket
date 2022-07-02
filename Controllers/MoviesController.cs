using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Services;

namespace web_movie.Controllers
{
    public class MoviesController : Controller
    {
        //// Inject AppDbContext
        //private readonly AppDbcontext _context;
        //public MoviesController(AppDbcontext context)
        //{
        //    _context = context;
        //}
        //public async Task<IActionResult> Index()
        //{
        //    var Allmovies = await _context.Movies.OrderByDescending(d => d.StartDate).Include(m => m.cinema).ToListAsync();
        //    return View(Allmovies);
        //}

        public IMoviesServices _services;
        public MoviesController(IMoviesServices _movies)
        {
            _services = _movies;
        }
        #region Trang chủ Movies
        public async Task<IActionResult> Index()
        {
            //var res = await _services.GetAllAsync(m=> m.cinema);
            var res = await _services.Get().Include(m => m.cinema).ToListAsync();
            return View(res);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            //var res = await _services.GetAllAsync(m=> m.cinema);
            var res = await _services.Get()
                .Include(m => m.cinema)
                .Include(m => m.producer)
                .Include(m => m.Actors_Movies).ThenInclude(m=>m.Actors)
                .FirstOrDefaultAsync(x => x.Id == id);
            return View(res);
        }
        #endregion
    }
}
