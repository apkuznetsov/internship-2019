using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealBlog.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Логин"),
            Required(ErrorMessage = "Введите логин"),
            MaxLength(20, ErrorMessage = "Максимальная длина – 20 символов"),
            MinLength(3, ErrorMessage = "Минимальная длина – 20 символов"),
            RegularExpression(@"^[a-zA-Z0-9_.-]{3,20}", ErrorMessage = "Спецсимволы недопустимы")]
        public string Login { get; set; }

        [Display(Name = "Пароль"),
            Required(ErrorMessage = "Введите пароль"),
            MaxLength(31, ErrorMessage = "Максимальная длина – 31 символ"),
            MinLength(6, ErrorMessage = "Минимальная длина – 6 символов"),
            RegularExpression(@"^[a-zA-Z0-9_.-]{6,31}", ErrorMessage = "Пароль может содержать только латинские буквы, цифры и подчёркивания")]
        public string Password { get; set; }

        [NotMapped,
            Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Почта"),
            Required(ErrorMessage = "Введите почту"),
            EmailAddress()]
        public string Email { get; set; }

        [Display(Name = "Изображение"),
            MaxLength(300, ErrorMessage = "Слишком длинный путь к изображению")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Имя"),
            MaxLength(31, ErrorMessage = "Максимальная длина поля – 31 символ")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"),
            MaxLength(31, ErrorMessage = "Максимальная длина поля – 31 символ")]
        public string LastName { get; set; }

        [Display(Name = "Контактная информация"),
            MaxLength(300, ErrorMessage = "Максимальная длина поля – 300 символов")]
        public string ContactInfo { get; set; }

        [Display(Name = "Характеристики мотоцикла"),
            MaxLength(300, ErrorMessage = "Максимальная длина поля – 300 символов")]
        public string BikeSpecifications { get; set; }

        [Display(Name = "Увлечения"),
            MaxLength(300, ErrorMessage = "мМксимальная длина поля – 300 символов")]
        public string Hobbies { get; set; }
    }
}