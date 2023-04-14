using Castle.DynamicProxy;
using CastleCoreSample;
using Shares;

namespace CastleDynamicProxySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var proxyGenerator = new ProxyGenerator();

            RapService rapService = proxyGenerator.CreateClassProxy<RapService>(new MyInterceptor());
            rapService.Rap();

            Console.WriteLine(new string('-', 20));

            ISpeakService speakService = proxyGenerator.CreateInterfaceProxyWithTarget<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();

            Console.WriteLine(new string('-', 20));

            speakService = proxyGenerator.CreateInterfaceProxyWithTargetInterface<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();
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