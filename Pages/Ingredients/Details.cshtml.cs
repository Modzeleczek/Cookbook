using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Ingredients
{
    public class DetailsModel : ShoppingCartPageModel
    {
        public Ingredient ingredient { get; set; }
        public DetailsModel(CookbookContext dataSource, DAL.ShoppingCart shoppingCart) : base(dataSource, shoppingCart) { }
        public void OnGet(int id)
        {
            ingredient = cookbookDB.GetIngredient(id);
        }
        public IActionResult OnPost(int id)
        {
            LoadCart();
            if(shoppingCart.Contains(id) == false)
                shoppingCart.Add(id); // dodajemy id produktu do koszyka
            SaveCart();
            // return RedirectToPage("/Ingredients/Details", new { id = id }); // 1. sposób; przekierowujemy z powrotem na stronę Details z parametrem id (takim samym, jak byśmy przechodzili ze strony List na stronę Details); po tym strona ponownie się ładuje i jest wykonywany OnGet
            // return LocalRedirect($"/Ingredients/Details?id={id}"); // 2. sposób
            OnGet(id); // ręcznie wykonujemy operacje z OnGet, bo OnGet nie wykonuje się automatycznie po return Page, tak jak by to było przy pierwszym przejściu na stronę
            return Page();
        }
    }
}
