using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest3
{
    /// <summary>
    /// 也可以在目标类上打Intercept特性，而不是在注册时指定
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

    public interface IRapService
    {
        void Rap();
    }

    [Intercept(typeof(MyInterceptor))]
    public class RapService : IRapService
    {
        public void Rap()
        {
            PrintHelper.Print("YoYoYo");
        }
    }
}
