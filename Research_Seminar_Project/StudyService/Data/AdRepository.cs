using StudyService.Models;

namespace StudyService.Data
{
    public class AdRepository
    {
        private static List<Ad> _ads = new List<Ad>
        {
            new Ad { Id = 1, Title = "Помогу с математикой", Description = "Репетитор", Type = Ad.AdType.Offer, UserId = 1, Price = 500, CreatedAt = DateTime.Now }
        };

        public List<Ad> GetAllAds() => _ads;
        public void AddAd(Ad ad) => _ads.Add(ad);

        public bool DeleteAd(int adId, int userId)
        {
            var ad = _ads.FirstOrDefault(a => a.Id == adId && a.UserId == userId);
            if (ad == null) return false;
            _ads.Remove(ad);
            return true;
        }
    }
}