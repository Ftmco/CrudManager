using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.GenericRepository.Services
{

    /// <summary>
    /// Repository TModel 
    /// </summary>
    /// <typeparam name="TModel">Model Entity</typeparam>
    public interface IGenericRepository<TModel> where TModel : class
    {        
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> where);
        Task<TModel> GetFirstOrDefualtAsync(Expression<Func<TModel, bool>> where);
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> where);
        Task<TModel> FindByIdAsync(object id);
        Task<bool> InsertAsync(TModel model);
        Task<bool> InsertAsync(IEnumerable<TModel> model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> UpdateAsync(IEnumerable<TModel> model);
        Task<bool> DeleteAsync(TModel model);
        Task<bool> DeleteAsync(IEnumerable<TModel> model);
        Task<bool> DeleteAsync(object id);
    }
}
