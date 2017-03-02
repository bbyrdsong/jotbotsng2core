using System;
using JotBotNg2Core.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                var result = GetModel(id) as IonResource;
                result.Meta = new IonLink
                {
                    Href = Url.Link("defaultApi", new {id = id}),
                    Relations = new[] {"GET"},
                    Datetime = DateTime.UtcNow
                };

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = GetModels();
                foreach (var obj in results)
                {
                    (obj as IonResource).Meta = new IonLink
                    {
                        Href = Url.Link("defaultApi", new {id = obj.Id}),
                        Relations = new[] {"GET"},
                        Datetime = DateTime.UtcNow
                    };
                }
                var response = new IonCollection<T>()
                {
                    Meta =
                        new IonLink
                        {
                            Href = Url.Link("defaultApi", null),
                            Relations = new[] {"GET", "collection"},
                            Datetime = DateTime.UtcNow
                        },
                    Items = results,
                    Elements = results.Count()
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] T model)
        {
            try
            {
                var result = PostModel(model);
                var forMeta = result as IonResource;

                forMeta.Meta = new IonLink
                {
                    Href = Url.Link("defaultApi", new {id = result.Id}),
                    Relations = new[] {"POST"},
                    Datetime = DateTime.UtcNow
                };

                return Ok(forMeta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] T model, int id)
        {
            try
            {
                var result = PutModel(model, id) as IonResource;
                result.Meta = new IonLink
                {
                    Href = Url.Link("defaultApi", new {id = model.Id}),
                    Relations = new[] {"PUT"},
                    Datetime = DateTime.UtcNow
                };

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var returnValue = DeleteModel(id);

                return Ok(returnValue);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        protected virtual T PutModel(T model, int id)
        {
            var result = Context.Modify(model, id);
            Context.Save();

            return result;
        }

        protected virtual int DeleteModel(int id)
        {
            Context.Delete<T>(id);
            return Context.Save();
        }
#endregion
    }
}