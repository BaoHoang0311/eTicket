using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Services;
using web_movie.Models;

namespace web_movie.Controllers
{
    public class ActorsController : Controller
    {
        //// tương tác với database
        //private readonly AppDbcontext _context;
        //public ActorsController(AppDbcontext context)
        //{
        //    _context = context;
        //}
        //public IActionResult Index()
        //{
        //    var allactors = _context.Actors.ToList().OrderBy(m=> m.FullName);
        //    return View(allactors);
        //}

        private readonly IActorServices _service;

        public ActorsController(IActorServices service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allactors = await _service.GetAllAsync();
            return View(allactors);
        }
        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        // Post Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePicture,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            // return name of index action
            return RedirectToAction(nameof(Index));
        }

        //Get : Actor/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorID = await _service.GetById(id);
            if (actorID == null) return View("Empty");
            return View(actorID);
        }
        // Get: Actor/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var actor_find = await _service.GetById(id);
            if (actor_find == null)
            {
                return View("Not Found");
            }
            return View(actor_find);
        }
        [BindProperty]
        public Actor actor { get; set; }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            var actor_new = await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }
        //Post: /Actors/Delete/id  /Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var Actor = await _service.GetById(id);
            if (Actor == null) return View("Not Found");
            return View(Actor);
        }
      [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete_X(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
