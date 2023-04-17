using Microsoft.AspNetCore.Mvc;
using ScrutorCastleDynamicProxyScanFullSample.AppServices.Account;

namespace ScrutorCastleDynamicProxyScanFullSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAccountAppService _service;

        public AccountController(ILogger<WeatherForecastController> logger,IAccountAppService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<bool> Create(UserAccountDto dto)
        {
            return await _service.Create(dto);
        }

        [HttpPut]
        public async Task<bool> Update(UserAccountDto dto)
        {
            return await _service.Update(dto);
        }
    }
}