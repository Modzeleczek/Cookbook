using System.ComponentModel.DataAnnotations;

namespace Cookbook.Models
{
    public class User
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Pole 'Nazwa użytkownika' jest wymagane.")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
        public string password { get; set; }
    }
}