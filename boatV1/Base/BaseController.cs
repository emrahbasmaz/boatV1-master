using Boat.Framework.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boatV1.Base
{
    [ApiController]
    public abstract class BaseController<TEntity, TPrimaryKey, TRepository, TService> : Microsoft.AspNetCore.Mvc.Controller
        where TEntity : class
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TService : IService<TEntity, TPrimaryKey, TRepository>
    {
        protected readonly TService Service;

        protected BaseController(TService service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual ActionResult<IEnumerable<TEntity>> Get()
        {
            IEnumerable<TEntity> entities = Service.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TEntity> Get([FromBody] TPrimaryKey id)
        {
            TEntity entity = Service.Get(id);
            return Ok(entity);
        }

        [HttpGet("GetAsync/{id}")]
        public async virtual Task<ActionResult<TEntity>> GetAsync([FromBody] TPrimaryKey id)
        {
            TEntity entity = await Service.GetAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        public virtual ActionResult<object> Add([FromBody]  TEntity entity)
        {
            var id = Service.Add(entity);
            return Ok(id);
        }

        [HttpPost("AddAsync")]
        public async virtual Task<ActionResult<object>> AddAsync([FromBody]  TEntity entity)
        {
            var id = await Service.AddAsync(entity);
            return Ok(id);
        }

        [HttpPut]
        public virtual ActionResult Update([FromBody] TEntity entity)
        {
            Service.Update(entity);
            return Ok();
        }

        [HttpPut("UpdateAsync")]
        public async virtual Task<ActionResult> UpdateAsync([FromBody]  TEntity entity)
        {
            await Service.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete([FromBody] TEntity entity)
        {
            Service.Delete(entity);
            return Ok();
        }

        [HttpDelete("DeleteAsync/{id}")]
        public async virtual Task<ActionResult> DeleteAsync([FromBody] TEntity entity)
        {
            await Service.DeleteAsync(entity);
            return Ok();
        }
    }
}
