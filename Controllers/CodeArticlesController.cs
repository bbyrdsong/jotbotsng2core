using JotBotNg2Core.Data;
using JotBotNg2Core.Lib;
using JotBotNg2Core.Models;

namespace JotBotNg2Core.Controllers
{
    public class CodeArticlesController: JotBotApiController<CodeArticle>
    {
        public CodeArticlesController(ApiDbContext context) : base(context) { }
    }
}