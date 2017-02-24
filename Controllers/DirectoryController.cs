using JotBotNg2Core.Data;
using JotBotNg2Core.Lib;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Controllers
{
    public class DirectoryController : JotBotApiController<Directory>
    {
        public DirectoryController(ApiDbContext context) : base(context) { }
    }
}