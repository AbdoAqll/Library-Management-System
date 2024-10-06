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
    public class ReturnRepository : GenericRepository<Return>, IReturnRepository
    {
        private readonly ApplicationDbContext context;

        public ReturnRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}