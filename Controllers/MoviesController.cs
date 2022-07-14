using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Services;
using web_movie.Data.Static;
using web_movie.Data.ViewModel;
using web_movie.Models;

namespace web_movie.Controllers
{
    [Authorize(Roles = Role_User.Admin)]
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
        [AllowAnonymous]
        #region Trang chủ Movies
        public async Task<IActionResult> Index()
        {
            var res = await _services.Get().Include(m => m.cinema).Include(m=>m.producer).ToListAsync();
            return View(res);
        }
        #endregion
        [AllowAnonymous]
        #region Search
        public async Task<IActionResult> Filter(string searchstring)
        {
            var all = await _services.Get().Include(m => m.cinema).ToListAsync();
            if (!string.IsNullOrEmpty(searchstring))
            {
                var result = all.Where(n => n.FullName.Contains(searchstring.ToLower())).ToList();
                return View("Index",result);
            }
            return View("Index",all);
        }
        #endregion
        [AllowAnonymous]
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            var res = await _services.GetMovieByID(id);
            return View(res);
        }
        #endregion

        [BindProperty]
        public NewMovieVM movies { get; set; }
        #region Create
        // Get: /Movies/Create
        public async Task<IActionResult> Create()
        {
            var res = await _services.Dropdown();
            ViewBag.Cinemas = new SelectList(res.Cinemas, "Id", "FullName");
            ViewBag.Actors = new SelectList(res.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(res.Producers, "Id", "FullName");
            return View();
        }
        // Post: /Movies/Create
        [HttpPost,ActionName("Create")]
        public async Task<IActionResult> Create(NewMovieVM movies)
        {
            if (!ModelState.IsValid)
            {
                return View(movies);
            }
            await _services.AddNewMovie(movies);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit
        // Get: /Movies/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var drop = await _services.Dropdown();
            ViewBag.Cinemas = new SelectList(drop.Cinemas, "Id", "FullName");
            ViewBag.Actors = new SelectList(drop.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(drop.Producers, "Id", "FullName");

            var res = await _services.GetMovieByID(id);

            NewMovieVM newmovie = new()
            {
                Id=res.Id,
                FullName = res.FullName,
                Description = res.Description,
                Price = res.Price,
                ImageUrl = res.ImageUrl,
                StartDate = res.StartDate,
                EndDay = res.EndDay,
                MovieCategory = res.MovieCategory,
                CinemaID = res.CinemaID,
                ProducerID = res.ProducerID,
                Ds_actor = res.Actors_Movies.Select(m => m.ActorId).ToList(),
            };

            // //c2
            //newmovie.Ds_actor = new();
            //foreach (var item in res.Actors_Movies)
            //{
            //    newmovie.Ds_actor.Add(item.ActorId);
            //}

            return View(newmovie);
        }
        [HttpPost,ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, NewMovieVM movies)
        {
            await _services.EditMovie(id, movies);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
