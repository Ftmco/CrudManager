using Dapper;
using FTeam.CrudManager.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FTeam.Services
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

        public async Task<CrudStatus> DeleteAsync(object id) => await Task.Run(async () => await DeleteAsync(await FindByIdAsync(id)));

        public async Task<TModel> FindByIdAsync(object id) => await Task.Run(async () =>
        {
            using SqlConnection connection = new(_db.Database.GetConnectionString());
            string entityName = GetTableName();
            TModel entity = await connection.QueryFirstAsync<TModel>($"");
            return entity;

        });

        public async Task<IEnumerable<TModel>> GetAllAsync() => await Task.Run(async () =>
        {
            using var connection = new SqlConnection(_db.Database.GetConnectionString());
            string entityName = GetTableName();
            IEnumerable<TModel> entityList = await connection.QueryAsync<TModel>($"SELECT * FROM {entityName}");
            return entityList;
        });

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.Where(where).ToListAsync());

        public async Task<TModel> GetFirstOrDefualtAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.FirstOrDefaultAsync(where));

        public async Task<bool> AnyAsync(Expression<Func<TModel, bool>> where) => await Task.Run(async () => await _dbSet.AnyAsync(where));

        public async Task<CrudStatus> DeleteAsync(IEnumerable<TModel> model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    _dbSet.RemoveRange(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        public async Task<CrudStatus> DeleteAsync(TModel model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    _dbSet.Remove(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        public async Task<CrudStatus> DeleteAsync(Expression<Func<TModel, bool>> deleteWhere)
            => await Task.Run(async () => await DeleteAsync(await GetAllAsync(deleteWhere)));

        public async Task<CrudStatus> InsertAsync(TModel model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    await _dbSet.AddAsync(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        public async Task<CrudStatus> InsertAsync(IEnumerable<TModel> model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    await _dbSet.AddRangeAsync(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        public async Task<CrudStatus> UpdateAsync(TModel model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    _dbSet.Update(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        public async Task<CrudStatus> UpdateAsync(IEnumerable<TModel> model)
            => await Task.Run(async () =>
            {
                if (model == null)
                    return CrudStatus.NullRefrence;

                try
                {
                    _dbSet.UpdateRange(model);
                    return await SaveChangesAsync() switch
                    {
                        SaveChangesStatus.Success => CrudStatus.Success,
                        _ => CrudStatus.Exception
                    };
                }
                catch
                {
                    return CrudStatus.Exception;
                }
            });

        private async Task<SaveChangesStatus> SaveChangesAsync()
            => await Task.Run(async () =>
            {
                try
                {
                    await _db.SaveChangesAsync();
                    return SaveChangesStatus.Success;
                }
                catch
                {
                    return SaveChangesStatus.Exception;
                }
            });

        private string GetTableName()
        {
            IEntityType entityType = _db.Model.FindEntityType(typeof(TModel));
            string entityName = entityType.GetTableName();
            return entityName;
        }
    }
}
