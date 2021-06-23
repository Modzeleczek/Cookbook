using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Admin.Dishes
{
    public class DeleteModel : DBPageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public DeleteModel(CookbookContext dataSource) : base(dataSource) { }
        public IActionResult OnGet()
        {
            cookbookDB.DeleteDish(id);
            return LocalRedirect("/Dishes/List");
        }
    }
}
