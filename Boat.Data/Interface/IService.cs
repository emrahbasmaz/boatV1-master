using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boat.Data.Interface
{
    /// <summary>
    /// All Service interface must implement this interface in order to Castle Windsor inject constructors
    /// </summary>
    public interface IService
    {

    }

    /// <summary>
    /// All Service interface must implement this interface in order to Castle Windsor inject constructors
    /// </summary>
    public interface IService<TEntity, TPrimaryKey, TRepository> : IService
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);
        long Add(TEntity entity);
        Task<long> AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
