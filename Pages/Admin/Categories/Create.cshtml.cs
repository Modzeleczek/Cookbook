using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Categories
{
    public class CreateModel : DBPageModel
    {
        [BindProperty]
        public Category newCategory { get; set; }
        public CreateModel(CookbookContext dataSource) : base(dataSource) { }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
                return Page(); // trzeba zwrócić Page, żeby było widać błędy walidacji (cena nie może być ujemna itp.)
            cookbookDB.AddCategory(newCategory);
            return LocalRedirect("/Categories/List");
        }
    }
}
