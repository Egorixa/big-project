namespace StudyService.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int AdId { get; set; } // ID объявления, на которое откликнулись
        public int UserId { get; set; } // ID пользователя, который откликнулся
        public string Message { get; set; } // Сообщение в отклике (например, "Я могу помочь!")
        public DateTime CreatedAt { get; set; } // Дата создания отклика
    }
}