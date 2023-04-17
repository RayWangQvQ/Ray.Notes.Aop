using FodySample.Attributes;
using Shares;

namespace FodySample.AppServices
{
    public class RapService: IRapService
    {
        [AppServiceInterceptor]
        public async Task Rap()
        {
            await Task.Delay(1000);
            PrintHelper.Print("YoYoYo");
        }
    }
}
