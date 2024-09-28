using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        void Update(Book book);
    }
}
