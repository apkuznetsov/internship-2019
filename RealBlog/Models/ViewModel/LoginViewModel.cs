using System.ComponentModel.DataAnnotations;

namespace RealBlog.Models.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Логин"),
            Required(ErrorMessage = "введите псевдоним"),
            MaxLength(20, ErrorMessage = "максимальная длина юзернэйма – 20 символов")]
        public string Login { get; set; }

        [Display(Name = "пароль"),
            Required(ErrorMessage = "введите пароль"),
            MaxLength(20, ErrorMessage = "максимальная длина пароля – 31 символ")]
        public string Password { get; set; }

        [Display(Name = "Запомнить пароль")]
        public bool RememberMe { get; set; }
    }
}