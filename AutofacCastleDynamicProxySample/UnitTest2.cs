using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest2
{
    /// <summary>
    /// 当服务不是interface时，也可以使用EnableClassInterceptors指定对类进行拦截
    /// 注意，但要求类的方法必须是virtual虚方法
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // 注册拦截器
        builder.RegisterType<MyInterceptor>();

        // 注册接口及其实现类
        builder.RegisterType<RapService>()
            .EnableClassInterceptors()
            .InterceptedBy(typeof(MyInterceptor));

        // 创建容器
        var container = builder.Build();
        // 解析服务
        var service = container.Resolve<RapService>();

        // 执行
        service.Rap();
    }

    public class RapService
    {
        public virtual void Rap()
        {
            PrintHelper.Print("YoYoYo");
        }
    }
}
