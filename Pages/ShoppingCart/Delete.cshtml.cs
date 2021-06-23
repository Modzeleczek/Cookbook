using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.ShoppingCart
{
    public class DeleteModel : ShoppingCartPageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public DeleteModel(CookbookContext dataSource, DAL.ShoppingCart shoppingCart) : base(dataSource, shoppingCart) { }
        public IActionResult OnGet()
        {
            LoadCart();
            shoppingCart.Remove(id);
            SaveCart();
            return LocalRedirect("/ShoppingCart/List");
        }
    }
}
