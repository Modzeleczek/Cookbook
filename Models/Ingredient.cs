using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cookbook.Models
{
    public class Ingredient
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Pole 'ID' jest wymagane.")]
        public int id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
        public string name { get; set; }

        [Display(Name = "Cena za gram")]
        [Required(ErrorMessage = "Pole 'Cena za gram' jest wymagane.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Cena nie może być ujemna.")]
        [DisplayFormat(DataFormatString = "{0:0.#####}", ApplyFormatInEditMode = true)] // bez tego wyświetla maksymalnie 2 cyfry po przecinku
        public decimal? price { get; set; }
        
        public List<DishIngredient> DishIngredient { get; set; } // właściwość nawigacji do tabeli DishIngredient
    }
}
