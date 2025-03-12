namespace StudyService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Пароль (в реальном проекте нужен хэш)
        public EducationStage Stage { get; set; }
        public int Course { get; set; }
        public string ProfileDescription { get; set; }

        public enum EducationStage
        {
            Бакалавр,
            Магистр
        }
    }
}