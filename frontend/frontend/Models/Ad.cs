using System;

namespace frontend.Models;

public class Ad
{
    public enum AdType
    {
        Request,
        Offer
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AdType Type { get; set; }
    public int UserId { get; set; }
    public decimal? Price { get; set; } // Цена (может быть null, если не указана)
    public DateTime CreatedAt { get; set; } // Дата создания
}