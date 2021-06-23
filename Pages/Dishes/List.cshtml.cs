using System.Collections.Generic;
using Cookbook.Models;
using Cookbook.DAL;
using System.Threading.Tasks;

namespace Cookbook.Pages.Dishes
{
    public class ListModel : DBPageModel
    {
        public List<Dish> dishes;
        public ListModel(CookbookContext dataSource) : base(dataSource) { }
        public async Task OnGet()
        {
            dishes = await cookbookDB.GetAllDishes();
            foreach (var dish in dishes)
                cookbookDB.Entry(dish).Reference(d => d.Category).Load();
        }
    }
}
