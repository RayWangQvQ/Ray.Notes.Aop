using Autofac;
using Autofac.Extras.DynamicProxy;

namespace AutofacCastleDynamicProxySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create builder
            var builder = new ContainerBuilder();

            // 注册接口及其实现类
            builder.RegisterType<RapService>()
                .As<IRapService>()
                .EnableInterfaceInterceptors();
            // 注册拦截器
            builder.Register(c => new MyInterceptor());

            // 创建容器
            var container = builder.Build();
            // 解析服务
            var service = container.Resolve<IRapService>();

            // 执行
            service.Rap();
        }
    }
}