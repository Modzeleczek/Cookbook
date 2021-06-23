using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cookbook.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Pole 'ID' jest wymagane.")]
        public int id { get; set; }

        [Display(Name = "Krótka nazwa")]
        [Required(ErrorMessage = "Pole 'Krótka nazwa' jest wymagane.")]
        public string shortName { get; set; }

        [Display(Name = "Długa nazwa")]
        [Required(ErrorMessage = "Pole 'Długa nazwa' jest wymagane.")]
        public string longName { get; set; }

        public List<Dish> Dish { get; set; } // właściwość nawigacji do tabeli Dish
    }
}
