using Shares;
using System.Reflection.Emit;
using System.Reflection;

namespace DiyStaticProxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //原本的，直接调用
            ISpeakService service = new SpeakService();
            service.Say();

            Console.WriteLine(new string('-', 20));

            //使用proxy
            ISpeakService proxy = new MyProxy();
            proxy.Say();
        }
    }
}