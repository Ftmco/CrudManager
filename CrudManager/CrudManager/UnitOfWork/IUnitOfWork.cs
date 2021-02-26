using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Services.Generic_Repository.UnitOfWork
{
    /// <summary>
    /// Db Context Controller
    /// </summary>
    /// <typeparam name="TContext">Db Context</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// Db Context
        /// </summary>
        public TContext DbContext { get; init; }

        /// <summary>
        /// Save All changes Async
        /// </summary>
        /// <returns>
        /// Success : True
        /// </returns>
        /// <exception cref="DbUpdateException">DbUpdateException</exception>
        Task<bool> SaveAsync();
    }
}
