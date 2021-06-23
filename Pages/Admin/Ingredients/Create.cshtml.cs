using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Ingredients
{
    public class CreateModel : DBPageModel
    {
        [BindProperty]
        public Ingredient newIngredient { get; set; }
        public CreateModel(CookbookContext dataSource) : base(dataSource) { }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
                return Page();
            cookbookDB.AddIngredient(newIngredient);
            return LocalRedirect("/Ingredients/List");
        }
    }
}
