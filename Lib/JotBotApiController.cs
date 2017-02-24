using JotBotNg2Core.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JotBotNg2Core.Lib
{
    
    [Route("api/[controller]")]
    public class JotBotApiController<T>: Controller where T: class, ISupportIdentity
    {
        protected IDataContext Context { get; }

#region Con/De-structor
        public JotBotApiController(ApiDbContext context)
        {
            Context = context;
        }
        ~JotBotApiController()
        {
            Context.Dispose();
        }
#endregion

#region Public - api
        [HttpGet("{id}")]
        public T Get(int id) 
        {
            return GetModel(id);
        }

        [HttpGet]
        public IEnumerable<T> Get()
        {
            return GetModels();
        }

        [HttpPost]
        public T Post(T model)
        {
            return PostModel(model);
        }

        [HttpPut]
        public T Put(T model)
        {
            return PutModel(model);
        }

        [HttpDelete]
        public void Delete(T model)
        {
            DeleteModel(model);
        }
#endregion

#region Protected - overridable
        protected virtual T GetModel(int id)
        {
            return Context.Find<T>(id);
        }

        protected virtual IEnumerable<T> GetModels()
        {
            return Context.GetAll<T>();
        }

        protected virtual T PostModel(T model)
        {
            var result = Context.Insert(model);
            Context.Save();                        

            return result;
        }

        protected virtual T PutModel(T model)
        {
            var result = Context.Modify(model);
            Context.Save();

            return result;
        }

        protected virtual void DeleteModel(T model)
        {
            Context.Delete(model);
            Context.Save();
        }
#endregion
    }
}