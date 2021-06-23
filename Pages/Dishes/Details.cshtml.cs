using Cookbook.Models;
using Cookbook.DAL;
using System.Collections.Generic;

namespace Cookbook.Pages.Dishes
{
    public class DetailsModel : ShoppingCartPageModel
    {
        public Dish dish { get; set; } // służy do wyświetlenia danych potrawy po załadowaniu strony (po OnGet)
        public Category category { get; set; }
        public ICollection<DishIngredient> dishIngredients { get; set; }
        public DetailsModel(CookbookContext dataSource, DAL.ShoppingCart shoppingCart) : base(dataSource, shoppingCart) { }
        public void OnGet(int id)
        {
            dish = cookbookDB.GetDish(id); // pobieramy z bazy danych potrawę o podanym id
            cookbookDB.Entry(dish).Reference(d => d.Category).Load(); // pobieramy z bazy danych kategorię potrawy (explicit loading; lazy loading nie zadziałał)
            category = dish.Category;
            cookbookDB.Entry(dish).Collection(d => d.DishIngredient).Load(); // pobieramy z bazy danych rekordy potrawa-składnik odpowiadające potrawie
            dishIngredients = dish.DishIngredient;
            foreach (var di in dishIngredients) // wczytujemy nazwy i ceny składników potrawy
                cookbookDB.Entry(di).Reference(di => di.Ingredient).Load();
        }
    }
}
