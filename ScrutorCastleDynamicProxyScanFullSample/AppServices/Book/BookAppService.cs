namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Book
{
    public class BookAppService : IBookAppService
    {
        private readonly ILogger<BookAppService> _logger;

        public BookAppService(ILogger<BookAppService> logger)
        {
            _logger = logger;
        }

        public async Task Create(BookDto dto)
        {
            await Task.Delay(100);
            _logger.LogInformation("这是创建书籍方法");
        }

        public async Task Update(BookDto dto)
        {
            await Task.Delay(100);
            _logger.LogInformation("这是更新用户方法");
        }
    }
}
