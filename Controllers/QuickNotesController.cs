using JotBotNg2Core.Data;
using JotBotNg2Core.Lib;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Controllers
{
    public class QuickNotesController : JotBotApiController<QuickNote>
    {
        public QuickNotesController(ApiDbContext context) : base(context) { }
    }
}