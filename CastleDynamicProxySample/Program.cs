using Castle.DynamicProxy;
using CastleCoreSample;
using Shares;

namespace CastleDynamicProxySample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var proxyGenerator = new ProxyGenerator();

            //类代理
            RapService rapService = proxyGenerator.CreateClassProxy<RapService>(new MyInterceptor());
            rapService.Rap();

            Console.WriteLine(new string('-', 20));

            //接口代理
            ISpeakService speakService = proxyGenerator.CreateInterfaceProxyWithTargetInterface<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();

            Console.WriteLine(new string('-', 20));

            speakService = proxyGenerator.CreateInterfaceProxyWithTarget<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();

            Console.WriteLine(new string('-', 20));

            //异步，会出问题
            ISingService singService = proxyGenerator.CreateInterfaceProxyWithTargetInterface<ISingService>(new SingService(), new MyInterceptor());
            await singService.Sing();

            Console.WriteLine(new string('-', 20));

            singService = proxyGenerator.CreateInterfaceProxyWithTargetInterface<ISingService>(new SingService(), new MyAsyncInterceptor());
            await singService.Sing();
        }
    }

    public class RapService
    {
        public virtual void Rap()
        {
            Console.WriteLine("YoYoYo");
        }
    }
}