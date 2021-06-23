using System.ComponentModel.DataAnnotations;

namespace Cookbook.Models
{
    public class DishIngredient
    {
        [Display(Name = "Ilość w gramach")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        // [DataType(DataType.Text, ErrorMessage = "Pole musi być liczbą dziesiętną.")]
        [DisplayFormat(DataFormatString = "{0:0.#####}", ApplyFormatInEditMode = true)]
        public decimal? ingredientAmount { get; set; } // ilość składnika w gramach

        public int dishId { get; set; }
        // public Dish Dish { get; set; } // właściwość nawigacji do tabeli Dish

        [Display(Name = "ID")]
        [Range(1, int.MaxValue, ErrorMessage = "Zaznacz istniejący składnik.")]
        public int ingredientId { get; set; }
        
        public Ingredient Ingredient { get; set; } // właściwość nawigacji do tabeli Ingredient
    }
}
