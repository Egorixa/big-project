using System;

namespace frontend.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}