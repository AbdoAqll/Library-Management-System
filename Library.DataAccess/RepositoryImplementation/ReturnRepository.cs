using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.RepositoryImplementation
{
    public class ReturnRepository : GenericRepository<Return>, IReturnRepository
    {
        private readonly ApplicationDbContext context;

        public ReturnRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Return>> GetAllReturnsFilterdByUsernnameAsync(string username)
        {
            return await context.Returns
               .Include(r => r.CheckOut)
               .Include(r => r.CheckOut.ApplicationUser)
               .Where(c => c.CheckOut.ApplicationUser.Name.ToLower().Contains(username) 
                && c.CheckOut.Status == StaticData.Returned)
               .ToListAsync();
        }
    }
}