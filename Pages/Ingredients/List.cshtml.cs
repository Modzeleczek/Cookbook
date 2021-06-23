using System.Collections.Generic;
using Cookbook.Models;
using Cookbook.DAL;
using System.Threading.Tasks;

namespace Cookbook.Pages.Ingredients
{
    public class ListModel : DBPageModel
    {
        public List<Ingredient> ingredients;
        public ListModel(CookbookContext dataSource) : base(dataSource) { }
        public async Task OnGet()
        {
            ingredients = await cookbookDB.GetAllIngredients();
        }
    }
}
