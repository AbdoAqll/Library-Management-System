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

        public async Task<IEnumerable<Penalty>> GetAllPenaltiesFilterdByUsernnameAsync(string username)
        {
            IQueryable<Penalty> query = context.Penalties
                     .Include(p => p.Return)
                         .ThenInclude(r => r.CheckOut)
                             .ThenInclude(c => c.Book)
                     .Include(p => p.Return)
                         .ThenInclude(r => r.CheckOut)
                             .ThenInclude(c => c.ApplicationUser)
                      .Where(p=> p.Return.CheckOut.ApplicationUser.Name.ToLower().Contains(username));
       
            return await query.ToListAsync();
        }

        public async Task<Penalty> GetFirstPenalty(int id)
        {
            var query = context.Penalties.Where(x => x.Id == id)
               .Include(p => p.Return)
                   .ThenInclude(r => r.CheckOut)
                       .ThenInclude(c => c.Book)
               .Include(p => p.Return)
                   .ThenInclude(r => r.CheckOut)
                       .ThenInclude(c => c.ApplicationUser);

            return (await query.FirstOrDefaultAsync())!;
        }
    }
}