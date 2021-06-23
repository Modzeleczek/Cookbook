using Microsoft.AspNetCore.Mvc.RazorPages;
using Cookbook.DAL;

namespace Cookbook.Models
{
    public class DBPageModel : PageModel
    {
        public CookbookContext cookbookDB { get; private set; }
        public DBPageModel(CookbookContext dataSource) // wstrzyknięcie zależności (dependency injection)
        {
            cookbookDB = dataSource;
        }
    }
}
