using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Services.Generic_Repository.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        public TContext DbContext { get; init; }

        Task<bool> SaveAsync();
    }
}
