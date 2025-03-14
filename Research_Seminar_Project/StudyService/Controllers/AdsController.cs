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
    private readonly ResponseRepository _responseRepository;

    public AdsController(AdRepository adRepository, ResponseRepository responseRepository)
    {
        _adRepository = adRepository;
        _responseRepository = responseRepository;
    }

    [HttpGet]
    public ActionResult<List<Ad>> GetAds()
    {
        return Ok(_adRepository.GetAllAds());
    }

    [HttpPost]
    public ActionResult<Ad> CreateAd([FromBody] Ad ad)
    {
        try
        {
            if (ad == null || string.IsNullOrEmpty(ad.Title))
                return BadRequest("Объявление должно иметь заголовок");
            _adRepository.AddAd(ad);
            Console.WriteLine("Ad added with ID: " + ad.Id);
            return CreatedAtAction(nameof(GetAds), new { id = ad.Id }, ad);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in CreateAd: " + ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteAd(int id, [FromQuery] int userId)
    {
        var deleted = _adRepository.DeleteAd(id, userId);
        if (!deleted)
            return NotFound("Объявление не найдено или вы не можете его удалить");
        _responseRepository.DeleteResponsesByAdId(id);
        return NoContent();
    }
}
}