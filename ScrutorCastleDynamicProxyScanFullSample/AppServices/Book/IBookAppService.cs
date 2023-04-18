namespace ScrutorCastleDynamicProxyScanFullSample.AppServices.Book
{
    public interface IBookAppService : IAppService
    {
        Task Create(BookDto dto);

        void Update(BookDto dto);
    }
}
