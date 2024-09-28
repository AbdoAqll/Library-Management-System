using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IBookRepository BookRepository { get; }
    }
}
