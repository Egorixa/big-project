using System.ComponentModel.DataAnnotations;

namespace FrontSite.Models;

public class User
{
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "Имя не может быть длиннее 30 символов.")]
    public string Name { get; set; }

    [StringLength(30, ErrorMessage = "Фамилия не может быть длиннее 30 символов.")]
    public string Surname { get; set; }

    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты.")]
    public string Email { get; set; }

    [StringLength(100, ErrorMessage = "Пароль не может быть длиннее 100 символов.")]
    public string Password { get; set; } 

    public EducationStage Stage { get; set; }

    [Range(1, 4, ErrorMessage = "Курс должен быть от 1 до 4 для бакалавра и от 1 до 2 для магистра.")]
    public int Course { get; set; }

    public string ProfileDescription { get; set; }

    public enum EducationStage
    {
        Бакалавр,
        Магистр
    }
}