using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly AdRepository _adRepository;

        public AdsController()
        {
            _adRepository = new AdRepository();
        }

        // GET: api/ads?title=интегралы&minPrice=0&maxPrice=100&startDate=2025-03-01
        [HttpGet]
        public ActionResult<List<Ad>> GetAds(
            [FromQuery] string? title = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] DateTime? startDate = null)
        {
            var ads = _adRepository.GetAllAds();

            // Фильтр по заголовку
            if (!string.IsNullOrEmpty(title))
            {
                ads = ads.Where(a => a.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Фильтр по цене
            if (minPrice.HasValue)
            {
                ads = ads.Where(a => a.Price.HasValue && a.Price >= minPrice.Value).ToList();
            }
            if (maxPrice.HasValue)
            {
                ads = ads.Where(a => a.Price.HasValue && a.Price <= maxPrice.Value).ToList();
            }

            // Фильтр по дате
            if (startDate.HasValue)
            {
                ads = ads.Where(a => a.CreatedAt >= startDate.Value).ToList();
            }

            return Ok(ads);
        }

        [HttpPost]
        public ActionResult<Ad> CreateAd([FromBody] Ad ad)
        {
            if (ad == null || string.IsNullOrEmpty(ad.Title))
            {
                return BadRequest("Объявление должно иметь заголовок");
            }

            _adRepository.AddAd(ad);
            return CreatedAtAction(nameof(GetAds), new { id = ad.Id }, ad);
        }
    }
}