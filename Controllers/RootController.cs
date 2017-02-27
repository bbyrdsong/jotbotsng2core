using JotBotNg2Core.Lib;
using Microsoft.AspNetCore.Mvc;

namespace JotBotNg2Core.Controllers
{
    [Route("api/")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new
            {
                meta = new IonLink { Href = Url.Link("deafultApi", null) },
                directory = new IonLink { Href = Url.Link("defaultApi", new { controller = "directory" }), Relations = new[] { "GET", "collection" } },
                documents = new IonLink { Href = Url.Link("defaultApi", new { controller = "documents" }), Relations = new[] { "GET", "collection" } },
                quicknotes = new IonLink { Href = Url.Link("defaultApi", new { controller = "quicknotes" }), Relations = new[] { "GET", "collection" } },
                worktasks = new IonLink { Href = Url.Link("defaultApi", new { controller = "worktasks" }), Relations = new[] { "GET", "collection" } }
            };

            return Ok(response);
        }
    }
}