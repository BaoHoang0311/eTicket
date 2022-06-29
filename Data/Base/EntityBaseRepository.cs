using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace web_movie.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T:class, IEntityID, new()
    {
        // tương tác với database
        // inject làm việc với database
        private readonly AppDbcontext _context;
        public EntityBaseRepository(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var data = await _context.Set<T>().ToListAsync();
            return data;
        }
        public async Task<bool> AddAsync(T entity)
        {
            var check = await _context.Set<T>().FirstOrDefaultAsync(m => m.FullName == entity.FullName);
            if (check != null)
            {
                return false;
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(int id)
        {
            var actors = await GetById(id);
            _context.Remove(actors);
            await _context.SaveChangesAsync();
        }
        public async Task<T> GetById(int id)
        {
            var actor = await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            return actor;
        }
        public async Task<T> UpdateAsync(int id, T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
