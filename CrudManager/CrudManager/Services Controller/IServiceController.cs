using Microsoft.EntityFrameworkCore;
using Services.GenericRepository.Services;
using System;
using System.Threading.Tasks;

namespace Services.GenericRepository.ServicesController
{
    public interface IServiceController<TModel,TContext> : IDisposable where TModel : class where TContext : DbContext
    {
        public IGenericRepository<TModel> Services { get; }

        Task<bool> SaveAsync();
    }
}
