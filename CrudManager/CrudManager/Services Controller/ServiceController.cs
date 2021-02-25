using Microsoft.EntityFrameworkCore;
using Services.Generic_Repository.UnitOfWork;
using Services.GenericRepository.Services;
using System.Threading.Tasks;

namespace Services.GenericRepository.ServicesController
{
    public class ServiceController<TModel, TContext> : IServiceController<TModel, TContext> where TModel : class where TContext : DbContext, new()
    {

        #region __Depndency__

        /// <summary>
        /// Db Context Controller
        /// </summary>
        private readonly IUnitOfWork<TContext> _unitOfwork;

        public ServiceController()
        {
            _unitOfwork = new UnitOfWork<TContext>();
        }

        #endregion

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
                {
                    _services = new GenericServices<TModel>(_unitOfwork.DbContext);
                }
                return _services;
            }
        }

        /// <summary>
        /// Dispose Db Context
        /// </summary>
        public void Dispose() =>
            _unitOfwork.Dispose();

        /// <summary>
        /// Save All Changes Async Use 'await' Befor Use This
        /// </summary>
        /// <returns>
        /// True = Success; Fasle = Exception
        /// </returns>
        public async Task<bool> SaveAsync() => await _unitOfwork.SaveAsync();
    }
}
