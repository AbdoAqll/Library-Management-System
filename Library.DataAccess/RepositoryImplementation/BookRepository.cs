using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.RepositoryImplementation
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public Task UpdateAsync(Book book)
        {
            dbSet.Attach(book);
            context.Entry(book).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
