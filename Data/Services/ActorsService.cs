using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Models;
using web_movie.Data.Base;

namespace web_movie.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorServices
    {
        public ActorsService(AppDbcontext context):base(context)
        {

        }
    }
}
