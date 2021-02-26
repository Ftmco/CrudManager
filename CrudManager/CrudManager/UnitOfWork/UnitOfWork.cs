using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Generic_Repository.UnitOfWork
{
    /// <summary>
    /// Impelement IUnitOfWork
    /// </summary>
    /// <typeparam name="TContext">Db Context</typeparam>
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
                //Save All Changes
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw dbUpdateException;
            }
        });
    }
}
