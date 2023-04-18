using Castle.DynamicProxy;

namespace AutofacCastleDynamicProxyFullSample.AppServices
{
    public class AsyncInterceptorAdaper<TAsyncInterceptor> : AsyncDeterminationInterceptor
        where TAsyncInterceptor : IAsyncInterceptor
    {
        public AsyncInterceptorAdaper(TAsyncInterceptor asyncInterceptor)
            : base(asyncInterceptor)
        { }
    }
}
