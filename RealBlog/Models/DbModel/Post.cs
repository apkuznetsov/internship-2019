using System;
using System.ComponentModel.DataAnnotations;

namespace RealBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Введите текст"),
            Required(ErrorMessage = "Введите текст"),
            MaxLength(4095, ErrorMessage = "Максимальная длина текста — 4095 символов")]
        public string Text { get; set; }

        [Display(Name = "Добавьте изображение"),
            Required(ErrorMessage = "Добавьте изображение")]
        public string PhotoUrl { get; set; }

        public virtual User Author { get; set; }
    }
}