using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}