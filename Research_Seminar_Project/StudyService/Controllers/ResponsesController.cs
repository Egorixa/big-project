using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsesController : ControllerBase
    {
        private readonly ResponseRepository _responseRepository;
        private readonly AdRepository _adRepository;

        public ResponsesController()
        {
            _responseRepository = new ResponseRepository();
            _adRepository = new AdRepository();
        }

        // GET: api/responses?adId=1
        [HttpGet]
        public ActionResult<List<Response>> GetResponses([FromQuery] int adId)
        {
            var responses = _responseRepository.GetResponsesByAdId(adId);
            return Ok(responses);
        }

        // POST: api/responses
        [HttpPost]
        public ActionResult<Response> CreateResponse([FromBody] Response response)
        {
            if (response == null || string.IsNullOrEmpty(response.Message))
            {
                return BadRequest("Сообщение в отклике обязательно");
            }

            // Проверяем, существует ли объявление
            var ad = _adRepository.GetAllAds().FirstOrDefault(a => a.Id == response.AdId);
            if (ad == null)
            {
                return NotFound("Объявление не найдено");
            }

            _responseRepository.AddResponse(response);
            return CreatedAtAction(nameof(GetResponses), new { adId = response.AdId }, response);
        }
    }
}