using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Pages.Admin.Dishes.IngredientActions
{
    public class AddModel : DBPageModel
    {
        [BindProperty(SupportsGet = true)]
        public string returnUrl { get; set; }
        public List<Ingredient> ingredients { get; set; }
        [BindProperty]
        public DishIngredient dishIngredient { get; set; }
        [BindProperty]
        public int ingredientId { get; set; }
        public AddModel(CookbookContext dataSource) : base(dataSource) { }
        public async Task OnGet()
        {
            var allIngredients = await cookbookDB.GetAllIngredients();
            var addedDishIngredients = TempData.Get<List<DishIngredient>>("dishIngredients");
            ingredients = Except(allIngredients, addedDishIngredients);
            TempData.Keep();
        }
        public async Task<IActionResult> OnPost(int button)
        {
            switch (button)
            {
                default: // button == 0
                    return Cancel();
                case 1:
                    return await Confirm();
            }
        }
        private IActionResult Cancel()
        {
            TempData.Keep();
            return LocalRedirect(returnUrl);
        }
        private async Task<IActionResult> Confirm()
        {
            if (ModelState.IsValid == false)
            {
                ingredientId = dishIngredient.ingredientId;
                await OnGet();
                return Page();
            }
            var addedDishIngredients = TempData.Get<List<DishIngredient>>("dishIngredients");
            addedDishIngredients.Add(dishIngredient);
            TempData.Set("dishIngredients", addedDishIngredients);
            TempData.Keep();
            return LocalRedirect(returnUrl);
        }
        private List<Ingredient> Except(List<Ingredient> first, List<DishIngredient> second)
        {
            List<Ingredient> res = new List<Ingredient>();
            foreach (var i in first)
            {
                bool isInSecond = false;
                foreach (var s in second)
                {
                    if (s.ingredientId == i.id)
                    {
                        isInSecond = true;
                        break;
                    }
                }
                if (isInSecond == false)
                    res.Add(i);
            }
            return res;
        }
    }
}
