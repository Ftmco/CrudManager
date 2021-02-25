using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Generic_Repository.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        #region __Get Dependency Injection__

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

        #endregion

        public async void Dispose() =>
            await DbContext.DisposeAsync();

        public async Task<bool> SaveAsync() => await Task.Run(async () =>
        {
            try
            {
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}
