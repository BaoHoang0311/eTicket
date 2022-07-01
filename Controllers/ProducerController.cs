using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.Services;
using web_movie.Models;

namespace web_movie.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerServices _services;
        public ProducerController(IProducerServices services)
        {
            _services = services;
        }

        #region Bind
        [BindProperty]
        public Producer producer { get; set; }
        #endregion

        #region Trang chủ Producer
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAllAsync();
            return View(result);
        }
        #endregion

        #region Create
        // Create
        // Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create_X()
        {
            var check = await _services.AddAsync(producer);
            if (!ModelState.IsValid) return View(producer);
            if (check == false) return View("Not Found");
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _services.GetById(id);
            return View(result);
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _services.GetById(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete_X(int id)
        {
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Create));
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _services.GetById(id);
            return View(result);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit_X(int id,Producer producer)
        {
            await _services.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
