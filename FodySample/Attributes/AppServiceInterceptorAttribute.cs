using Rougamo;
using Rougamo.Context;
using Shares;

namespace FodySample.Attributes
{
    public class AppServiceInterceptorAttribute : MoAttribute
    {
        private ILogger<AppServiceInterceptorAttribute> _logger;

        public AppServiceInterceptorAttribute()
        {
            //想依赖注入的话，这里只能使用ServiceLocator模式
            using var scope = MyStaticClass.ServiceProvider.CreateScope();
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<AppServiceInterceptorAttribute>>();
        }

        public override void OnEntry(MethodContext context)
        {
            PrintHelper.Print("before...");
        }

        public override void OnExit(MethodContext context)
        {
            PrintHelper.Print("after...");
        }
    }
}
