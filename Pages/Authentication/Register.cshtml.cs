using Microsoft.AspNetCore.Mvc;
using Cookbook.DAL;
using Cookbook.Models;

namespace Cookbook.Pages.Authentication
{
    public class RegisterModel : UserPageModel
    {
        public RegisterModel(UserSqlDB userDB) : base(userDB) { }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
                return Page();
            if (userDB.Exists(user.userName) == true)
            {
                errorMessage = "Użytkownik o podanej nazwie już istnieje.";
                return Page();
            }
            user.password = HashPassword(user.password); // zamieniamy wpisane hasło na hash
            userDB.Add(user);
            return LocalRedirect(returnUrl); // LocalRedirect zamiast RedirectToPage używamy, kiedy zamiast hard-codowanego URLa mamy returnUrl, na którego wartość ma wpływ użytkownik
        }
    }
}
