﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data;

namespace web_movie.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbcontext _context;
        public ProducerController(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var listproducers = await _context.Producers.ToListAsync();
            return View("Index",listproducers);
        }
    }
}
