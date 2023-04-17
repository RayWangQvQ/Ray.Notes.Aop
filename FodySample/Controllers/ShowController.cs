using FodySample.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace FodySample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly ILogger<ShowController> _logger;
        private readonly IRapService _rapService;

        public ShowController(ILogger<ShowController> logger, IRapService rapService)
        {
            _logger = logger;
            _rapService = rapService;
        }

        [HttpGet]
        public async Task<bool> Get()
        {
            await _rapService.Rap();
            return true;
        }
    }
}