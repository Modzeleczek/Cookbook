using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cookbook.DAL;
using System.Text;
using System.Security.Cryptography;

namespace Cookbook.Models
{
    public class UserPageModel : PageModel
    {
        public UserSqlDB userDB { get; private set; }
        // https://stackoverflow.com/questions/52693364/asp-net-core-2-1-razor-page-return-page-with-model
        [BindProperty(SupportsGet = true)]
        public string returnUrl { get; set; }
        [BindProperty]
        public User user { get; set; }
        public string errorMessage { get; set; }
        public UserPageModel(UserSqlDB userDB) // wstrzyknięcie zależności (dependency injection)
        {
            this.userDB = userDB;
        }
        public void OnGet()
        {
            returnUrl = returnUrl ?? "/";
            errorMessage = "";
        }
        protected string HashPassword(string password) // https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)); // hash obliczony sha256 zawsze ma dokładnie 32 bajty (32 * 8 b = 256 b)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2")); // jeżeli chcemy zapisać hash z sha256 w postaci napisu, to każdy bajt hasha zamieniamy na 2 cyfry szesnastkowe
                return builder.ToString();
            }
        }
    }
}
