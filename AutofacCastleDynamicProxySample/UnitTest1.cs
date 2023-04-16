using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest1
{
    /// <summary>
    /// ע������ʱ��ֱ��ͨ��InterceptedBy��ָ��AOP������
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // ע��������
        builder.RegisterType<MyInterceptor>();

        // ע��ӿڼ���ʵ����
        builder.RegisterType<SpeakService>()
            .As<ISpeakService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(MyInterceptor));

        // ��������
        var container = builder.Build();
        // ��������
        var service = container.Resolve<ISpeakService>();

        // ִ��
        service.Say();
    }
}