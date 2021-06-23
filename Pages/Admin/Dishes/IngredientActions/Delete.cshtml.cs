using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Pages.Admin.Dishes.IngredientActions
{
    public class DeleteModel : DBPageModel
    {
        public DeleteModel(CookbookContext dataSource) : base(dataSource) { }
        public IActionResult OnGet(int id, string returnUrl)
        {
            var addedDishIngredients = TempData.Get<List<DishIngredient>>("dishIngredients");
            var toRemove = addedDishIngredients.Find(di => (di.ingredientId == id));
            if (toRemove != null)
            {
                addedDishIngredients.Remove(toRemove);
                TempData.Set("dishIngredients", addedDishIngredients);
            }
            TempData.Keep();
            return LocalRedirect(returnUrl);
        }
    }
}
