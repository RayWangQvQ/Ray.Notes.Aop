using Microsoft.AspNetCore.Mvc;
using Shares;

namespace ScrutorDecoratorSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly ILogger<ShowController> _logger;
        private readonly ISpeakService _speakService;
        private readonly ISingService _singService;

        public ShowController(
            ILogger<ShowController> logger, 
            ISpeakService speakService,
            ISingService singService
            )
        {
            _logger = logger;
            _speakService = speakService;
            _singService = singService;
        }

        [HttpGet]
        public bool Get()
        {
            _speakService.Say();
            _singService.Sing();
            return true;
        }
    }
}