using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Library_Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.RepositoryImplementation
{
    internal class CheckoutRepository : GenericRepository<Checkout>, ICheckoutRepository
    {
        private readonly ApplicationDbContext context;

        public CheckoutRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Checkout>> GetAllCheckoutsFilterdByUsernnameAsync(string username)
        {
            return await context.Checkouts.Include(c => c.ApplicationUser)
                .Where(c => c.ApplicationUser.Name.ToLower().Contains(username) && c.Status == StaticData.ConfirmedByUser)
                .ToListAsync(); 
        }
    }
}