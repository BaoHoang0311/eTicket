using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Data.ViewModel;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class MoviesServices : EntityBaseRepository<Movie>, IMoviesServices
    {
        private readonly AppDbcontext _context;
        public MoviesServices(AppDbcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<MovieDropDown> Dropdown()
        {
            var responce = new MovieDropDown()
            {
                Actors = await _context.Actors.ToListAsync(),
                Cinemas = await _context.Cinemas.ToListAsync(),
                Producers = await _context.Producers.ToListAsync()
            };
            return responce;
        }
        public async Task<Movie> GetMovieByID(int id)
        {
            var res = await _context.Movies
                        .Include(m => m.cinema).ThenInclude(m=>m.images)
                        .Include(m => m.producer)
                        .Include(m => m.Actors_Movies).ThenInclude(m => m.Actors)
                        .FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
        public async Task AddNewMovie(NewMovieVM data)
        {
            // // C1: vẫn tự lưu vào bảng Actor_Movies
            //Movie movie = new Movie();
            //movie = data;

            //movie.Actors_Movies = new List<Actor_Movie>();
            //foreach (var masodienvien in data.Ds_actor)
            //{
            //    Actor_Movie dv = new Actor_Movie()
            //    {
            //        ActorId = masodienvien,
            //    };
            //    movie.Actors_Movies.Add(dv);
            //}
            //await _context.Movies.AddAsync(movie);
            //await _context.SaveChangesAsync();

            // // C2: code trực quan hơn
            Movie movie = new Movie();
            movie = data;
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            foreach (var masodienvien in data.Ds_actor)
            {
                Actor_Movie dv = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = masodienvien
                };
                _context.Actors_Movies.Add(dv);
            }
            //Save bảng actor_movies
            await _context.SaveChangesAsync();
        }

        public async Task EditMovie(int id, NewMovieVM newmovieVM)
        {
            var data = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (data == null) return;

            var res = await _context.Actors_Movies.Where(m => m.MovieId == id).ToListAsync();
            _context.Actors_Movies.RemoveRange(res);
            await _context.SaveChangesAsync();
            
            data.FullName = newmovieVM.FullName;
            data.Description = newmovieVM.Description;
            data.Price = newmovieVM.Price;
            data.ImageUrl = newmovieVM.ImageUrl;
            data.StartDate = newmovieVM.StartDate;
            data.EndDay = newmovieVM.EndDay;
            data.MovieCategory = newmovieVM.MovieCategory;
            data.CinemaID = newmovieVM.CinemaID;
            data.ProducerID = newmovieVM.ProducerID;

            foreach (var masodienvien in newmovieVM.Ds_actor)
            {
                Actor_Movie dv = new Actor_Movie()
                {
                    ActorId = masodienvien
                };
                data.Actors_Movies.Add(dv);
            }

            await _context.SaveChangesAsync();
        }
    }
}
