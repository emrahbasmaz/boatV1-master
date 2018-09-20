using Boat.Framework.Interface;
using Boat.Framework.Ioc;
using Boat.Framework.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Boat.Framework.GenericRepository
{
    public class GenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
         where TEntity : class
    {
        protected IStringLocalizer<string> Localizer => IocFacility.Container.Resolve<IStringLocalizer<string>>();

        public IEnumerable<TEntity> Query(string sql)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return SqlMapper.Query<TEntity>(sqlConnection, sql).AsList();
        }

        public TEntity Get(TPrimaryKey key)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return SqlMapperExtensions.Get<TEntity>(sqlConnection, key);
        }

        public async Task<TEntity> GetAsync(TPrimaryKey key)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return await SqlMapperExtensions.GetAsync<TEntity>(sqlConnection, key);
        }

        public long Save(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return SqlMapperExtensions.Insert(sqlConnection, entity);
        }

        public async Task<long> SaveAsync(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return await SqlMapperExtensions.InsertAsync(sqlConnection, entity);
        }

        public bool Update(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return SqlMapperExtensions.Update(sqlConnection, entity);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return await SqlMapperExtensions.UpdateAsync(sqlConnection, entity);
        }

        public bool Delete(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return SqlMapperExtensions.Delete(sqlConnection, entity);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(DbConstant.DatabaseConnection))
                return await SqlMapperExtensions.DeleteAsync(sqlConnection, entity);
        }

    }
}
