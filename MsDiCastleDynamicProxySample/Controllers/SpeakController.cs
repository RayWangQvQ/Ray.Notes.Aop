using Microsoft.AspNetCore.Mvc;
using Shares;

namespace MsDiDynamicProxySample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISpeakService _speakService;

        public SpeakController(ILogger<WeatherForecastController> logger, ISpeakService speakService)
        {
            _logger = logger;
            _speakService = speakService;
        }

        [HttpGet]
        public bool Get()
        {
            _speakService.Say();
            return true;
        }
    }
}