namespace StudyService.Data
{
    public class AdRepository
    {
        private static List<Models.Ad> _ads = new List<Models.Ad>
        {
            new Models.Ad { Id = 1, Title = "Ищу помощь с интегралами", Description = "Не понимаю тему", Type = 0, UserId = 1, CreatedAt = DateTime.Now }
        };

        public List<Models.Ad> GetAllAds() => _ads;

        public void AddAd(Models.Ad ad)
        {
            ad.Id = _ads.Count + 1;
            ad.CreatedAt = DateTime.Now; // Устанавливаем дату создания
            _ads.Add(ad);
        }
    }
}