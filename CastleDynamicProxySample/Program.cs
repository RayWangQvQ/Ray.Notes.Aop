using Castle.DynamicProxy;
using Shares;

namespace CastleCoreSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var proxyGenerator = new ProxyGenerator();

            SingService singService = proxyGenerator.CreateClassProxy<SingService>(new MyInterceptor());
            singService.Sing();

            Console.WriteLine(new string('-', 20));

            ISpeakService speakService = proxyGenerator.CreateInterfaceProxyWithTarget<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();

            Console.WriteLine(new string('-', 20));

            speakService = proxyGenerator.CreateInterfaceProxyWithTargetInterface<ISpeakService>(new SpeakService(), new MyInterceptor());
            speakService.Say();
        }
    }

    public class SingService
    {
        public virtual void Sing()
        {
            Console.WriteLine("LaLaLa");
        }
    }
}