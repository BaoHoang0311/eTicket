using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class ActorsService : IActorServices
    {
        // tương tác với database
        // inject làm việc với database
        private readonly AppDbcontext _context;
        public ActorsService(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var data = await _context.Actors.ToListAsync();
            return data;
        }

        public async Task AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actors =await GetById(id);
            _context.Remove(actors);
            await _context.SaveChangesAsync();
        }

        public async Task<Actor> GetById(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(m=> m.Id==id);
            return actor;
        }

        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            _context.Update(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }

    }
}
