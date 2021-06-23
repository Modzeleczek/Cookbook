using System.Collections.Generic;
using Cookbook.Models;
using Cookbook.DAL;
using System.Threading.Tasks;

namespace Cookbook.Pages.Categories
{
    public class ListModel : DBPageModel
    {
        public List<Category> categories;
        public ListModel(CookbookContext dataSource) : base(dataSource) { }
        public async Task OnGet()
        {
            categories = await cookbookDB.GetAllCategories();
            foreach (var c in categories)
            {
                if (c.longName.Length > 64)
                    c.longName = c.longName.Substring(0, 61) + "...";
            }
        }
    }
}
