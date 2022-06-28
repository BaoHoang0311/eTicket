using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public interface IActorServices
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetById(int id);
        Task AddAsync(Actor actor);
        Task<Actor> UpdateAsync(int id, Actor newActor);
        Task DeleteAsync(int id);
    }
}
