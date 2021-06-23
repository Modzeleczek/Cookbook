using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Ingredients
{
    public class DeleteModel : DBPageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public DeleteModel(CookbookContext dataSource) : base(dataSource) { }
        public IActionResult OnGet()
        {
            cookbookDB.DeleteIngredient(id);
            return LocalRedirect("/Ingredients/List");
        }
    }
}
