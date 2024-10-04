using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
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
       
    }
}
