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

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await context.Books.ToListAsync(); // return all books if no search string is provided
            }

            searchString = searchString.ToLower(); // Convert search string to lowercase

            return await context.Books
                .Where(x => x.Title.ToLower().Contains(searchString) ||
                             x.Genre.ToLower().Contains(searchString) ||
                             x.Description.ToLower().Contains(searchString))
                .ToListAsync();
        }


        public Task UpdateAsync(Book book)
        {
            dbSet.Attach(book);
            context.Entry(book).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
