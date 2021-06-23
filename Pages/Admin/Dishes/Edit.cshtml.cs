using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Cookbook.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Pages.Admin.Dishes
{
    public class EditModel : DishAdminPageModel
    {
        public EditModel(CookbookContext dataSource) : base(dataSource) { }
        public async Task OnGet(int id)
        {
            var storedDish = TempData.Get<Dish>("dish"); // pobieramy dane potrawy z TempData
            if (storedDish == null) // jeżeli jeszcze nie zapisaliśmy żadnych w TempData, to storedDish == null
                dish = cookbookDB.GetDish(id); // zapisujemy dane potrawy z bazy danych w dish do zrenderowania na stronie
            else
                dish = storedDish; // w przeciwnym przypadku zapisujemy pobrane dane w dish

            categories = await cookbookDB.GetAllCategories(); // pobieramy wszystkie kategorie z bazy danych
            var storedCategoryId = TempData["categoryId"]; // pobieramy wybrany id kategorii z TempData
            if (storedCategoryId == null) // jeżeli jeszcze nie zapisaliśmy go w TempData, to storedCategoryId == null
            {
                cookbookDB.Entry(dish).Reference(d => d.Category).Load();
                categoryId = dish.Category.id; // ustawiamy kategorię potrawy z bazy danych w categoryId do zrenderowania na stronie
            }
            else
                categoryId = (int)storedCategoryId; // w przeciwnym przypadku zapisujemy pobrany id w categoryId

            dishIngredients = TempData.Get<List<DishIngredient>>("dishIngredients"); // pobieramy składniki potrawy z TempData
            if (dishIngredients == null) // jeżeli jeszcze nie zapisaliśmy żadnych w TempData, to dishIngredients == null
            {
                cookbookDB.Entry(dish).Collection(d => d.DishIngredient).Load();
                dishIngredients = dish.DishIngredient; // wczytujemy listę składników z bazy danych
                TempData.Set("dishIngredients", dishIngredients); // serializujemy ją i zapisujemy w TempData
            }
            foreach (var di in dishIngredients) // dla każdego wybranego składnika
                di.Ingredient = cookbookDB.GetIngredient((int)di.ingredientId); // pobieramy jego nazwę i cenę z bazy danych

            TempData.Keep(); // oznaczamy do zachowania wszystkie klucze w TempData
        }
        public async Task<IActionResult> OnPost(int button, string currentPath)
        {
            switch (button)
            {
                case 0:
                    return AddIngredient(currentPath);
                default: // submitButton == 1
                    return Cancel();
                case 2:
                    return await Confirm();
            }
        }
        private IActionResult AddIngredient(string currentPath)
        {
            TempData.Set("dish", dish); // serializujemy i zapisujemy zbindowany obiekt potrawy (nazwę i przepis)
            TempData["categoryId"] = categoryId; // zapisujemy zbindowany id kategorii
            TempData.Keep();
            return RedirectToPage("/Admin/Dishes/IngredientActions/Add", new
            {
                returnUrl = currentPath
            });
        }
        private IActionResult Cancel()
        {
            TempData.Clear(); // czyścimy TempData
            return LocalRedirect("/Dishes/List");
        }
        private async Task<IActionResult> Confirm()
        {
            if (ModelState.IsValid == false)
            {
                await OnGet(dish.id); // przygotowujemy stronę do wyświetlenia
                return Page(); // trzeba zwrócić Page, żeby było widać błędy walidacji
            }
            if (imageFile != null) // jeżeli użytkownik wybrał obraz
            {
                string type = imageFile.ContentType;
                if (type != "image/jpeg" && type != "image/jpg") // plik nie jest obrazem jpg
                {
                    imageError = "Wybierz plik będący obrazem .jpg lub .jpeg.";
                    await OnGet(dish.id);
                    return Page();
                }
                UpdateDish(); // aktualizujemy rekord potrawy w bazie danych
                string path = $"dish_img/{dish.id}.jpg";
                if (System.IO.File.Exists(path) == true)
                    System.IO.File.Delete(path); // usuwamy aktualne zdjęcie potrawy o danym id
                using (var fileStream = System.IO.File.Create(path)) // tworzymy nowy plik ze zdjęciem potrawy
                    imageFile.CopyTo(fileStream);
            }
            else
                UpdateDish();
            TempData.Clear();
            return LocalRedirect("/Dishes/List");
        }
        private void UpdateDish()
        {
            dish.Category = cookbookDB.GetCategory(categoryId); // pobieramy zaznaczoną kategorię z bazy danych i ustawiamy ją potrawie
            dish.DishIngredient = TempData.Get<List<DishIngredient>>("dishIngredients"); // pobieramy składniki z TempData i ustawiamy je potrawie
            cookbookDB.UpdateDish(dish); // aktualizujemy potrawę
        }
    }
}
