namespace AutofacCastleDynamicProxyFullSample.AppServices.Book
{
    public interface IBookAppService : IAppService
    {
        Task Create(BookDto dto);

        Task Update(BookDto dto);
    }
}
