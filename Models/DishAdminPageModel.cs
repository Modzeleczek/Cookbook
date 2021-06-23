using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cookbook.DAL;

namespace Cookbook.Models
{
    public class DishAdminPageModel : DBPageModel
    {
        public List<Category> categories { get; set; }
        public List<Ingredient> ingredients { get; set; }
        [BindProperty]
        public Dish dish { get; set; }
        [Display(Name = "Prześlij zdjęcie")]
        [BindProperty]
        public IFormFile imageFile { get; set; }
        public string imageError { get; set; }
        [BindProperty]
        [Display(Name = "Kategoria")]
        public int categoryId { get; set; } // przy przejściu do strony Create z innej strony nie używamy selectedCategoryId; przy przesyłaniu formularza (w OnPost), jeżeli wystąpi błąd walidacji, w selectedCategoryId zapisujemy id kategorii wybranej przez użytkownika
        public List<DishIngredient> dishIngredients { get; set; }
        public DishAdminPageModel(CookbookContext dataSource) : base(dataSource) { }
    }
}