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

        public ResponsesController(ResponseRepository responseRepository, AdRepository adRepository = null)
        {
            _responseRepository = responseRepository;
            _adRepository = adRepository;
        }

        [HttpGet]
        public ActionResult<List<Response>> GetResponses([FromQuery] int adId)
        {
            return Ok(_responseRepository.GetResponsesByAdId(adId));
        }

        [HttpPost]
        public ActionResult<Response> CreateResponse([FromBody] Response response)
        {
            if (response == null || string.IsNullOrEmpty(response.Message))
                return BadRequest("Сообщение в отклике обязательно");
            _responseRepository.AddResponse(response);
            return CreatedAtAction(nameof(GetResponses), new { adId = response.AdId }, response);
        }
    }
}