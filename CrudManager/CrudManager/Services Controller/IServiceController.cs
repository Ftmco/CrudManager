using Microsoft.EntityFrameworkCore;
using Services.GenericRepository.Services;
using System;
using System.Threading.Tasks;

namespace FTeam.ServicesController
{
    /// <summary>
    /// Crud Services Controller 
    /// </summary>
    /// <typeparam name="TModel">T Entity</typeparam>
    /// <typeparam name="TContext">T Db Context</typeparam>
    public interface IServiceController<TModel, TContext> : IDisposable where TModel : class where TContext : DbContext
    {
        /// <summary>
        /// Db Context
        /// </summary>
        public TContext DbContext { get; init; }

        /// <summary>
        /// Crud Services
        /// </summary>
        public IGenericRepository<TModel> Services { get; }

        /// <summary>
        /// Save TModel and TContext Changes Async 
        /// Use 'await' befor this
        /// </summary>
        /// <returns>
        /// True : Success
        /// False : Exception
        /// </returns>
        Task<bool> SaveAsync();

    }
}
