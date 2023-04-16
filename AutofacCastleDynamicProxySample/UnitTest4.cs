using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest4
{
    /// <summary>
    /// Intercept特性可以打在interface上
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // 注册拦截器
        builder.Register(c => new MyInterceptor());

        // 注册接口及其实现类
        builder.RegisterType<RapService>()
            .As<IRapService>()
            .EnableInterfaceInterceptors();

        // 创建容器
        var container = builder.Build();
        // 解析服务
        var service = container.Resolve<IRapService>();

        // 执行
        service.Rap();
    }

    [Intercept(typeof(MyInterceptor))]
    public interface IRapService
    {
        void Rap();
    }

    public class RapService : IRapService
    {
        public void Rap()
        {
            PrintHelper.Print("YoYoYo");
        }
    }
}
