using Microsoft.EntityFrameworkCore;
using Services.GenericRepository.Services;
using System.Threading.Tasks;

namespace FTeam.ServicesController
{
    public class ServiceController<TModel, TContext> : IServiceController<TModel, TContext> where TModel : class where TContext : DbContext, new()
    {
        /// <summary>
        /// Db Context
        /// </summary>
        private DbContext _db;

        public TContext DbContext
        {
            get
            {
                if (_db == null)
                {
                    _db = new TContext();
                }
                return (TContext)_db;
            }
            init => _db = new TContext();
        }

        /// <summary>
        /// Services Provider
        /// </summary>
        private IGenericRepository<TModel> _services;

        /// <summary>
        /// Model Service Provider
        /// </summary>
        public IGenericRepository<TModel> Services
        {
            get
            {
                if (_services == null)
                    _services = new GenericServices<TModel>(DbContext);

                return _services;
            }
        }     

        /// <summary>
        /// Dispose Db Context
        /// </summary>
        public async void Dispose() =>
           await _db.DisposeAsync();

        /// <summary>
        /// Save All Changes Async Use 'await' Befor Use This
        /// </summary>
        /// <returns>
        /// True = Success
        /// </returns>
        ///<exception cref="DbUpdateException">DbUpdateException</exception>
        public async Task<bool> SaveAsync() => await Task.Run(async () =>
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}
