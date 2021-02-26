using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.GenericRepository.Services
{
    /// <summary>
    /// Crud Services 
    /// Impelement IGenericRepository
    /// </summary>
    /// <typeparam name="TModel">T Entity</typeparam>
    public class GenericServices<TModel> : IGenericRepository<TModel> where TModel : class
    {
        #region __Dependency__

        /// <summary>
        /// Db Context
        /// </summary>
        private readonly DbContext _db;

        /// <summary>
        /// Db Set / Db Table
        /// </summary>
        private readonly DbSet<TModel> _dbSet;

        /// <summary>
        /// Generic Service 
        /// </summary>
        /// <param name="db">Db Context</param>
        public GenericServices(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TModel>();
        }

        #endregion

        public async Task<bool> DeleteAsync(object id) => await Task.Run(async () => await DeleteAsync(await FindByIdAsync(id)));

        public async Task<TModel> FindByIdAsync(object id) => await Task.Run(async () => await _dbSet.FindAsync(id));

        public async Task<IEnumerable<TModel>> GetAllAsync() => await Task.Run(async () => await _dbSet.ToListAsync());

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.Where(where).ToListAsync());

        public async Task<TModel> GetFirstOrDefualtAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.FirstOrDefaultAsync(where));

        public async Task<bool> AnyAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.AnyAsync(where));

        public async Task<bool> DeleteAsync(IEnumerable<TModel> model) => await Task.Run(() =>
        {
            try
            {
                _dbSet.RemoveRange(model);
                return true;
            }
            catch
            {
                throw;
            }
        });

        public async Task<bool> DeleteAsync(TModel model) => await Task.Run(() =>
        {
            try
            {
                _dbSet.Remove(model);
                return true;
            }
            catch
            {
                throw;
            }
        });


        public async Task<bool> InsertAsync(TModel model) => await Task.Run(async () =>
        {
            try
            {
                await _dbSet.AddAsync(model);
                return true;
            }
            catch(OperationCanceledException operationCanceledException)
            {
                throw operationCanceledException;
            }
        });

        public async Task<bool> InsertAsync(IEnumerable<TModel> model) => await Task.Run(async () =>
        {
            try
            {
                await _dbSet.AddRangeAsync(model);
                return true;
            }
            catch (OperationCanceledException operationCanceledException)
            {
                throw operationCanceledException;
            }
        });

        public async Task<bool> UpdateAsync(TModel model) => await Task.Run(() =>
        {
            try
            {
                _dbSet.Update(model);
                return true;
            }
            catch
            {
                throw;
            }
        });

        public async Task<bool> UpdateAsync(IEnumerable<TModel> model) => await Task.Run(() =>
        {
            try
            {
                _dbSet.UpdateRange(model);
                return true;
            }
            catch
            {
                throw;
            }
        });

    }
}
