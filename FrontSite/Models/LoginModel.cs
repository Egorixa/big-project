using System.ComponentModel.DataAnnotations;

namespace FrontSite.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Электронная почта обязательна для ввода.")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен для ввода.")]
    [StringLength(100, ErrorMessage = "Пароль должен быть длиной от 1 до 100 символов.", MinimumLength = 1)]
    public string Password { get; set; }
}