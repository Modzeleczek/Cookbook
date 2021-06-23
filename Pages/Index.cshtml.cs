using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cookbook.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return LocalRedirect("/Dishes/List");
        }
    }
}
