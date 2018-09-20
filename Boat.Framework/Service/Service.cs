using Boat.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boat.Framework.Service
{
    public abstract class Service<TEntity, TPrimaryKey, TRepository>
       : IService<TEntity, TPrimaryKey, TRepository>
       where TEntity : class
       where TRepository : IRepository<TEntity, TPrimaryKey>
    {
        protected readonly TRepository Repository;

        public Service(TRepository repository)
        {
            Repository = repository;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            string sql = "";
            return Repository.Query(sql);
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            return Repository.Get(id);
        }

        public async virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await Repository.GetAsync(id);
        }

        public virtual long Add(TEntity entity)
        {
            return Repository.Save(entity);
        }

        public async virtual Task<long> AddAsync(TEntity entity)
        {
            return await Repository.SaveAsync(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Repository.DeleteAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Repository.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Repository.UpdateAsync(entity);
        }
    }
}
