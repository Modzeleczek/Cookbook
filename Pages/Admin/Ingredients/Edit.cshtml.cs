using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Ingredients
{
    public class EditModel : DBPageModel
    {
        [BindProperty]
        public Ingredient ingredient { get; set; }
        public EditModel(CookbookContext dataSource) : base(dataSource) { }
        public void OnGet(int id) // wypełniamy pola formularza aktualnymi danymi kategorii
        {
            ingredient = cookbookDB.GetIngredient(id);
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
                return Page();
            cookbookDB.UpdateIngredient(ingredient);
            return LocalRedirect("/Ingredients/List");
        }
    }
}
