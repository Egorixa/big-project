using Microsoft.EntityFrameworkCore;
using StudyService.Models;

namespace StudyService.Data
{
    public class AdRepository
    {
        private readonly StudyServiceDbContext _context;

        public AdRepository(StudyServiceDbContext context)
        {
            _context = context;
        }

        public List<Ad> GetAllAds() => _context.Ads.ToList();

        public void AddAd(Ad ad)
        {
            if (ad.CreatedAt.Kind != DateTimeKind.Utc)
                ad.CreatedAt = DateTime.SpecifyKind(ad.CreatedAt, DateTimeKind.Utc);
            _context.Ads.Add(ad);
            _context.SaveChanges();
        }

        public bool DeleteAd(int adId, int userId)
        {
            var ad = _context.Ads.FirstOrDefault(a => a.Id == adId && a.UserId == userId);
            if (ad == null) return false;
            _context.Ads.Remove(ad);
            _context.SaveChanges();
            return true;
        }
    }
}