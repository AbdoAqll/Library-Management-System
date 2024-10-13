using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Repositories
{
    public interface ICheckoutRepository : IGenericRepository<Checkout>
    {
        public Task<IEnumerable<Checkout>> GetAllCheckoutsFilterdByUsernnameAsync(string username);
    }
}
