using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.RepositoryImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IBookRepository BookRepository { get; private set; }

        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

        public ICheckoutRepository CheckoutRepository{ get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.BookRepository = new BookRepository(context);
            this.ApplicationUserRepository = new ApplicationUserRepository(context, userManager);
            this.CheckoutRepository = new CheckoutRepository(context);
        }

        

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
