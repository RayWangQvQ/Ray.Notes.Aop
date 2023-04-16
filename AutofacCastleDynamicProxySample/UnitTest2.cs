using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest2
{
    /// <summary>
    /// ��������interfaceʱ��Ҳ����ʹ��EnableClassInterceptorsָ�������������
    /// ע�⣬��Ҫ����ķ���������virtual�鷽��
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // ע��������
        builder.RegisterType<MyInterceptor>();

        // ע��ӿڼ���ʵ����
        builder.RegisterType<RapService>()
            .EnableClassInterceptors()
            .InterceptedBy(typeof(MyInterceptor));

        // ��������
        var container = builder.Build();
        // ��������
        var service = container.Resolve<RapService>();

        // ִ��
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
