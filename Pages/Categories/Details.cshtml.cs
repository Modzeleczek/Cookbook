using Cookbook.Models;
using Cookbook.DAL;

namespace Cookbook.Pages.Categories
{
    public class DetailsModel : DBPageModel
    {
        public Category category { get; set; }
        public DetailsModel(CookbookContext dataSource) : base(dataSource) { }
        public void OnGet(int id)
        {
            category = cookbookDB.GetCategory(id);
        }
    }
}
