using Library.DataAccess.Data;
using Library.Entities.Models;
using Library.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.RepositoryImplementation
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(Book book)
        {
            context.Books.Update(book);
        }
    }
}
