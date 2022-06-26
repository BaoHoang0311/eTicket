using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;

namespace web_movie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbcontext _context;
        public MoviesController(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Allmovies = await _context.Movies.OrderByDescending(d=>d.StartDate).Include(m=>m.cinema).ToListAsync();
            return View(Allmovies);
        }
    }
}
