using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.ShoppingCart
{
    public class ListModel : ShoppingCartPageModel
    {
        public Stack<Ingredient> ingredientList;
        public bool anyDeleted { get; set; }
        public ListModel(CookbookContext dataSource, DAL.ShoppingCart shoppingCart) : base(dataSource, shoppingCart) { }
        public void OnGet()
        {
            LoadCart();
            var ids = shoppingCart.List();
            ingredientList = new Stack<Ingredient>();
            anyDeleted = false;
            for (int i = ids.Count - 1; i >= 0; --i) // iterujemy od końca, aby móc jednocześnie usuwać id nieistniejące w bazie danych
            {
                var found = cookbookDB.Ingredient.Find(ids[i]); // szukamy składnika o i-tym id z ciasteczka koszyka
                if (found != null)
                    ingredientList.Push(found); // wypełniamy listę produktów, łącząc identyfikatory z koszyka ze składnikami z bazy danych
                else
                {
                    anyDeleted = true;
                    ids.RemoveAt(i); // jeżeli składnik o aktualnym id został usunięty z bazy danych, to usuwamy go z koszyka
                }
            }
            SaveCart(); // zapisujemy koszyk w razie, gdyby zostały usunięte jakieś składniki
        }
        public IActionResult OnPost()
        {
            LoadCart();
            shoppingCart.Clear();
            SaveCart();
            return LocalRedirect("/ShoppingCart/List"); // po LocalRedirect wykonuje się OnGet tej strony; jeżeli damy tu return Page(), to OnGet się nie wykonuje
        }
    }
}
