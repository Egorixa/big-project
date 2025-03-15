using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace frontend.Models;

public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    [StringLength(30, ErrorMessage = "Имя не может быть длиннее 30 символов.")]
    public string Name { get; set; }

    [JsonPropertyName("surname")]
    [StringLength(30, ErrorMessage = "Фамилия не может быть длиннее 30 символов.")]
    public string Surname { get; set; }

    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты.")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    [StringLength(100, ErrorMessage = "Пароль не может быть длиннее 100 символов.")]
    public string Password { get; set; } 

    [JsonPropertyName("stage")]
    public EducationStage Stage { get; set; }

    [JsonPropertyName("course")]
    [Range(1, 4, ErrorMessage = "Курс должен быть от 1 до 4 для бакалавра и от 1 до 2 для магистра.")]
    public int Course { get; set; }

    [JsonPropertyName("profileDescription")]
    public string ProfileDescription { get; set; }

    public enum EducationStage
    {
        [JsonPropertyName("Бакалавр")]
        Бакалавр,
        
        [JsonPropertyName("Магистр")]
        Магистр
    }
}