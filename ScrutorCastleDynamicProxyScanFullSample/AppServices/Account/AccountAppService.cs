namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Account
{
    public class AccountAppService : IAccountAppService
    {
        private readonly ILogger<AccountAppService> _logger;

        public AccountAppService(ILogger<AccountAppService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> Create(UserAccountDto dto)
        {
            await Task.Delay(100);
            _logger.LogInformation("这是创建用户方法");
            return true;
        }

        public async Task<bool> Update(UserAccountDto dto)
        {
            await Task.Delay(100);
            _logger.LogInformation("这是更新用户方法");

            return true;
        }
    }
}
