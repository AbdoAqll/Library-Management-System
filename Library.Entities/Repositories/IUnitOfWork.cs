﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IBookRepository BookRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        ICheckoutRepository CheckoutRepository { get; }
        IReturnRepository ReturnRepository { get; }
        IPenaltyRepository PenaltyRepository { get; }
    }
}