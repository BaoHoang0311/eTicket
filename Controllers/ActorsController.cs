using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;

namespace web_movie.Controllers
{
    public class ActorsController : Controller
    {
        // tương tác với database
        private readonly AppDbcontext _context;
        public ActorsController(AppDbcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allactors = _context.Actors.ToList().OrderBy(m=> m.FullName);
            return View(allactors);
        }
    }
}
