using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Models
{
    public class Dish
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Pole 'ID' jest wymagane.")]
        public int id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
        [TempData]
        public string name { get; set; }

        [Display(Name = "Przepis")]
        [Required(ErrorMessage = "Pole 'Przepis' jest wymagane.")]
        [TempData]
        public string recipe { get; set; }

        [Display(Name = "Kategoria")]
        public Category Category { get; set; } // właściwość nawigacji do tabeli Category
        public List<DishIngredient> DishIngredient { get; set; } // właściwość nawigacji do tabeli DishIngredient
    }
}
