using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boat.Data.Interface
{
    /// <summary>
    /// All Repository interface must implement this interface in order to Castle Windsor inject constructors
    /// </summary>
    public interface IRepository
    {

    }

    /// <summary>
    /// All Repository interface must implement this interface in order to Castle Windsor inject constructors
    /// </summary>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class
    {
        IEnumerable<TEntity> Query(string sql);
        TEntity Get(TPrimaryKey key);
        Task<TEntity> GetAsync(TPrimaryKey key);

        long Save(TEntity entity);
        Task<long> SaveAsync(TEntity entity)
;
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
