using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;
using web_movie.Data.IFormFile;
using web_movie.Data.Services;
using web_movie.Data.Static;
using web_movie.Models;


namespace web_movie.Controllers
{
    [Authorize(Roles = Role_User.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaServices _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CinemasController(ICinemaServices service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Bind
        [BindProperty]
        public Cinema_prop cinema { get; set; }
        #endregion
        [AllowAnonymous]
        #region Trang chủ Actor
        public async Task<IActionResult> Index()
        {
            var results = await _service.GetAllAsync();
            return View(results);
        }
        #endregion

        #region Create
        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post Create
        [HttpPost]
        public async Task<IActionResult> Create(Cinema_prop cinema_prop)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema_prop);
            }

            if (ModelState.IsValid)
            {
                if (cinema_prop.Gallery_FormFiles != null)
                {
                    string folder = "movies/gallery/";

                    cinema_prop.Gallery = new List<ImageCinemas>();

                    foreach (var file in cinema_prop.Gallery_FormFiles)
                    {
                        var image = new ImageCinemas()
                        {
                            //Url
                            FullName = await UploadImage(folder, file)
                        };
                        cinema_prop.Gallery.Add(image);
                    }
                }
            }

            Cinema cinema = new Cinema()
            {
                Logo = cinema_prop.Logo,
                FullName = cinema_prop.FullName,
                Description = cinema_prop.Description
            };
            cinema.images = cinema_prop.Gallery;

            var check = await _service.AddAsync(cinema);

            if (check == false) return View("Not Found");
            return RedirectToAction(nameof(Index));
        }
        // tạo đường dẫn lưu vào wwwroot
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
        #endregion
        [AllowAnonymous]
        #region Details
        //Get : Cinemas/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var res = await _service.GetCinemasID(id);
            return View(res);
        }
        #endregion

        #region Edit
        // Get: Cinemas/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var cinemas = await _service.GetById(id);
            if (cinemas == null)
            {
                return View("Not Found");
            }
            return View(cinemas);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (cinema == null)
            {
                return View(cinema);
            }
            var actor_new = await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        //Post: /Cinemas/Delete/id ~~ /Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetCinemasID(id);
            if (cinema == null) return View("Not Found");
            return View(cinema);
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
