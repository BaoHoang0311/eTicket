using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public interface ICinemaServices : IEntityBaseRepository<Cinema> 
    {
        Task<Cinema> GetCinemasID(int id);
    }
}
