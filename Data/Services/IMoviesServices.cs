using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Models;
using web_movie.Data.ViewModel;

namespace web_movie.Data.Services
{
    public interface IMoviesServices : IEntityBaseRepository<Movie>
    {
        Task<MovieDropDown> Dropdown();
        Task<Movie> GetMovieByID(int id);
        Task AddNewMovie(NewMovieVM newmovieVM);
    }
}
