using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class MoviesServices : EntityBaseRepository<Movie>, IMoviesServices
    {
        private readonly AppDbcontext _context;
        public MoviesServices(AppDbcontext context):base(context)
        {
            _context = context;
        } 
    }
}
