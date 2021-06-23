using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Categories
{
    public class EditModel : DBPageModel
    {
        [BindProperty]
        public Category category { get; set; }
        public EditModel(CookbookContext dataSource) : base(dataSource) { }
        public void OnGet(int id) // wypełniamy pola formularza aktualnymi danymi kategorii
        {
            category = cookbookDB.GetCategory(id);
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
                return Page();
            cookbookDB.UpdateCategory(category);
            return LocalRedirect("/Categories/List");
        }
    }
}
