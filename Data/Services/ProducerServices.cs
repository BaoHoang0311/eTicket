using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Base;
using web_movie.Models;

namespace web_movie.Data.Services
{
    public class ProducerServices : EntityBaseRepository<Producer> ,IProducerServices
    {
        public ProducerServices(AppDbcontext context) :base (context)
        {
                
        }
    }
}
