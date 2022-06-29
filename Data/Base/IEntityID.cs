using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_movie.Data.Base
{
    public interface IEntityID
    {
        int Id { get; set; }
        string FullName { get; set; }
    }
}
