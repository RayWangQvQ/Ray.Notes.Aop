using Castle.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            PrintHelper.Print("咳咳咳");
            invocation.Proceed();
            PrintHelper.Print("谢谢");
        }
    }
}
