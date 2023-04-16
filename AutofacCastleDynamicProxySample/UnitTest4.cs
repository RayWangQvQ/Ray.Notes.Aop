using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest4
{
    /// <summary>
    /// Intercept���Կ��Դ���interface��
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // ע��������
        builder.Register(c => new MyInterceptor());

        // ע��ӿڼ���ʵ����
        builder.RegisterType<RapService>()
            .As<IRapService>()
            .EnableInterfaceInterceptors();

        // ��������
        var container = builder.Build();
        // ��������
        var service = container.Resolve<IRapService>();

        // ִ��
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
