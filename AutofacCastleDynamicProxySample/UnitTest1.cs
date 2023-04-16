using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest1
{
    /// <summary>
    /// 注册服务的时候，直接通过InterceptedBy来指定AOP拦截器
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // 注册拦截器
        builder.RegisterType<MyInterceptor>();

        // 注册接口及其实现类
        builder.RegisterType<SpeakService>()
            .As<ISpeakService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(MyInterceptor));

        // 创建容器
        var container = builder.Build();
        // 解析服务
        var service = container.Resolve<ISpeakService>();

        // 执行
        service.Say();
    }
}