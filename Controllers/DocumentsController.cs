using JotBotNg2Core.Data;
using JotBotNg2Core.Lib;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Controllers
{
    public class DocumentsController : JotBotApiController<Document>
    {
        public DocumentsController(ApiDbContext context) : base(context) { }
    }
}