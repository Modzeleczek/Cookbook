using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace Cookbook.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string returnUrl { get; set; }
        public async Task<IActionResult> OnGet()
        {
            returnUrl = returnUrl ?? "/";
            await HttpContext.SignOutAsync("CookieAuthentication");
            return LocalRedirect(returnUrl);
        }
    }
}
