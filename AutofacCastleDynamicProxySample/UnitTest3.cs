using Autofac;
using Autofac.Extras.DynamicProxy;
using Shares;

namespace AutofacCastleDynamicProxySample;

public class UnitTest3
{
    /// <summary>
    /// Ҳ������Ŀ�����ϴ�Intercept���ԣ���������ע��ʱָ��
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
