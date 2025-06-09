using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Role { get; set; } // "Client" или "Staff"
    }
}