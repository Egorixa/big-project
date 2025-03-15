namespace StudyService.Models
{
    public class Ad
    {
        public enum AdType
        {
            Request,
            Offer
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
