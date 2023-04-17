namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Account
{
    public interface IAccountAppService : IAppService
    {
        Task<bool> Create(UserAccountDto dto);

        Task<bool> Update(UserAccountDto dto);
    }
}
