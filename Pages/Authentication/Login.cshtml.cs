using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Cookbook.DAL;
using Cookbook.Models;

namespace Cookbook.Pages.Authentication
{
    public class LoginModel : UserPageModel
    {
        public LoginModel(UserSqlDB userDB) : base(userDB) { }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false) // walidacja obiektu User sprawdzi tylko, czy nie podano pustej nazwy lub hasła, które byłyby zbindowane jako nulle w obiekcie user, co sprawiłoby, że wywołanie procedury składowanej w GetPassword wyrzuci wyjątek
                return Page();
            string correctPassword = userDB.GetPassword(user.userName);
            if (correctPassword == null) // użytkownik o podanej nazwie nie istnieje w bazie
            {
                errorMessage = "Niepoprawna nazwa użytkownika lub hasło.";
                return Page();
            }
            string hashed = HashPassword(user.password);
            if (hashed != correctPassword) // jeżeli hash wpisanego hasła jest różny od zapisanego w bazie danych
            {
                errorMessage = "Niepoprawna nazwa użytkownika lub hasło.";
                return Page();
            } // pomyślna walidacja użytkownika
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.userName) };
            var identity = new ClaimsIdentity(claims, "CookieAuthentication");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("CookieAuthentication", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
            return LocalRedirect(returnUrl);
        }
    }
}
