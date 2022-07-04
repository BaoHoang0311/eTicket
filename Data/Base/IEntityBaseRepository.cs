using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace web_movie.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        DbSet<T> Get();
        Task<T> GetById(int id);
        Task<bool> AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
