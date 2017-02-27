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
        public IActionResult Get(int id) 
        {
            var result = GetModel(id) as IonResource;
            result.Meta = new IonLink { Href = Url.Link("defaultApi", new { id = id }), Relations = new[] { "GET" }};

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = GetModels();
            foreach (var obj in results)
            {
                (obj as IonResource).Meta = new IonLink { Href = Url.Link("defaultApi", new { id = obj.Id }), Relations = new[] { "GET" }};
            }
            var response = new IonCollection<T>()
            {
                Meta = new IonLink { Href = Url.Link("defaultApi", null), Relations = new[] { "GET", "collection"}},
                Items = results
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post(T model)
        {
            var result = PostModel(model);
            var forMeta = result as IonResource;

            forMeta.Meta = new IonLink { Href = Url.Link("defaultApi", new { id = result.Id }), Relations = new[]{"POST"}};

            return Ok(forMeta);
        }

        [HttpPut]
        public IActionResult Put(T model)
        {
            var result = PutModel(model) as IonResource;
            result.Meta = new IonLink { Href = Url.Link("defaultApi", new { id = model.Id }), Relations = new[] {"PUT"}};

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(T model)
        {
            DeleteModel(model);

            return Ok();
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