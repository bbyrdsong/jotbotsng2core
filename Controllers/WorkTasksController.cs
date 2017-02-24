using JotBotNg2Core.Data;
using JotBotNg2Core.Lib;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Controllers
{
    public class WorkTasksController : JotBotApiController<WorkTask>
    {
        public WorkTasksController(ApiDbContext context) : base(context) { }
    }
}