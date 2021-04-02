using FTeam.CrudManager.Response;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FTeam.Services
{

    /// <summary>
    /// Repository TModel 
    /// </summary>
    /// <typeparam name="TModel">Model Entity</typeparam>
    public interface IGenericRepository<TModel> where TModel : class
    {
        /// <summary>
        /// Get All TEntity Objects
        ///<see langword="await"/>
        /// </summary>
        /// <returns>IEnumerable<TModel></returns>
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Get All TEntity Objects With Where Expression
        /// <see langword="await"/>
        /// </summary>
        /// <param name="where"></param>
        /// <returns>IEnumerable<TModel></returns>
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> where);

        /// <summary>
        /// Find First Or Default TMoldel Object
        /// <see langword="await"/>
        /// </summary>
        /// <param name="firstOrDefault">First Or Default Expression</param>
        /// <returns><![CDATA[TModel]]></returns>
        Task<TModel> GetFirstOrDefualtAsync(Expression<Func<TModel, bool>> firstOrDefault);

        /// <summary>
        /// Check Exist Any Object 
        /// <see langword="await"/>
        /// </summary>
        /// <param name="any">Any Expression</param>
        /// <returns><![CDATA[TModel]]></returns>
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> any);

        /// <summary>
        /// Find TModel Object 
        /// <see langword="await"/>
        /// </summary>
        /// <param name="id">TModel Id</param>
        /// <returns><![CDATA[TModel]]></returns>
        Task<TModel> FindByIdAsync(object id);

        /// <summary>
        /// Insert Object TModel To DB
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">TModel Object</param>
        /// <returns>
        /// True : Success
        /// </returns>
        /// <exception cref="OperationCanceledException">OperationCanceledException</exception>
        Task<bool> InsertAsync(TModel model);

        /// <summary>
        /// Insert List TModel To DB
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">List TModel</param>
        /// <returns>
        /// True : Success
        /// </returns>
        ///<exception cref="OperationCanceledException">OperationCanceledException</exception>
        Task<bool> InsertAsync(IEnumerable<TModel> model);

        /// <summary>
        /// Update TModel 
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">TModel Object</param>
        /// <returns>
        /// True : Success
        /// </returns>
        Task<bool> UpdateAsync(TModel model);

        /// <summary>
        /// Update List TModel
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">List TModel</param>
        /// <returns>
        /// True : Success
        /// False : Exception
        /// </returns>
        Task<bool> UpdateAsync(IEnumerable<TModel> model);

        /// <summary>
        /// Delete TModel
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">TModel Object</param>
        /// <returns>
        /// True : Success
        /// </returns>
        Task<DeleteStatus> DeleteAsync(TModel model);

        /// <summary>
        /// Delete List TModel
        /// <see langword="await"/>
        /// </summary>
        /// <param name="model">List TModel</param>
        /// <returns>
        /// True : Success
        /// </returns>
        Task<DeleteStatus> DeleteAsync(IEnumerable<TModel> model);

        /// <summary>
        /// Delete List With Expression 
        /// </summary>
        /// <param name="deleteWhere">Delete Where Expression</param>
        /// <returns>True : Success</returns>
        Task<DeleteStatus> DeleteAsync(Expression<Func<TModel, bool>> deleteWhere);


        /// <summary>
        /// Delete TModel By Id
        /// <see langword="await"/>
        /// </summary>
        /// <param name="id">TModel Id</param>
        /// <returns>
        /// True : Success
        /// </returns>
        Task<bool> DeleteAsync(object id);
    }
}
