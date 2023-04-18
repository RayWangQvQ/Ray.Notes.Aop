using AutofacCastleDynamicProxyFullSample.AppServices.Book;
using Microsoft.AspNetCore.Mvc;

namespace AutofacCastleDynamicProxyFullSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBookAppService _service;

        public BookController(ILogger<WeatherForecastController> logger,IBookAppService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<bool> Create(BookDto dto)
        {
            await _service.Create(dto);
            return true;
        }

        [HttpPut]
        public async Task<bool> Update(BookDto dto)
        {
            await _service.Update(dto);
            return true;
        }
    }
}