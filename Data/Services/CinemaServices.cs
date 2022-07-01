using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class CinemaServices : EntityBaseRepository<Cinema>, ICinemaServices
    {
        private readonly AppDbcontext _context;
        public CinemaServices(AppDbcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cinema> GetCinemasID(int id)
        {
            var res = await _context.Cinemas
                .Include(m => m.Movies)
                .Include(m => m.images)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return res;
        }
    }
}
