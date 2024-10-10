using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library.DataAccess.RepositoryImplementation
{
    public class PenaltyRepository : GenericRepository<Penalty>, IPenaltyRepository
    {
        private readonly ApplicationDbContext context;

        public PenaltyRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Penalty>> GetAllPenalties()
        {
            IQueryable<Penalty> query = context.Penalties
               .Include(p => p.Return)
                   .ThenInclude(r => r.CheckOut)
                       .ThenInclude(c => c.Book)
               .Include(p => p.Return)
                   .ThenInclude(r => r.CheckOut)
                       .ThenInclude(c => c.ApplicationUser);

            return await query.ToListAsync();
        }
    }
}