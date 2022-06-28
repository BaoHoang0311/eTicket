﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
