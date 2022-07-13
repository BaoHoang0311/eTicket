using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Services;
using web_movie.Data.Static;
using web_movie.Models;

namespace web_movie.Controllers
{
    [Authorize(Roles = Role_User.Admin)]
    public class ActorsController : Controller
    {

        private readonly IActorServices _service;

        public ActorsController(IActorServices service)
        {
            _service = service;
        }
        [AllowAnonymous]
        #region Trang chủ Actor
        public async Task<IActionResult> Index()
        {
            var allactors = await _service.GetAllAsync();
            return View(allactors);
        }
        #endregion

        #region Create
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
            var check = await _service.AddAsync(actor);
            if(check==false) return View("Not Found");
            // return name of index action
            return RedirectToAction(nameof(Index));
        }
        #endregion

        [AllowAnonymous]
        #region Details
        //Get : Actor/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorID = await _service.GetById(id);
            if (actorID == null) return View("Empty");
            return View(actorID);
        }
        #endregion

        #region Edit
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
        #endregion

        #region Delete
        //Post: /Actors/Delete/id ~~ /Actors/Delete/1
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
        #endregion
    }
}
