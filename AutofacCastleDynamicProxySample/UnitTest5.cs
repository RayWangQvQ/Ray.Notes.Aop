using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Shares;
using System.Reflection;

namespace AutofacCastleDynamicProxySample;

public class UnitTest5
{
    /// <summary>
    /// �����Զ������ԣ�ʵ�ֶ����ھ��巽��������
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // ע��������
        builder.RegisterType<CallLoggerMethodInterceptor>();

        // ע��ӿڼ���ʵ����
        builder.RegisterType<RapService>()
            .As<IRapService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(CallLoggerMethodInterceptor));

        // ��������
        var container = builder.Build();
        // ��������
        var service = container.Resolve<IRapService>();

        // ִ��
        service.Rap();
        service.Dance();//û�ã����ܴ���interface�ϣ���Ϊ���������õ���invocation��RapService
    }

    public interface IRapService
    {
        void Rap();

        [CallLogger]
        void Dance();
    }

    public class RapService : IRapService
    {
        [CallLogger]
        public void Rap()
        {
            PrintHelper.Print("YoYoYo");
        }

        public void Dance()
        {
            PrintHelper.Print("����˹����...");
        }
    }

    public class CallLoggerMethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            MethodInfo mi = invocation.MethodInvocationTarget
                            ?? invocation.Method;

            //���Ŀ�꺯��û�����ָ�������ԣ���ֱ��ִ��Ŀ�꺯��
            if (mi.GetCustomAttributes<CallLoggerAttribute>(true).FirstOrDefault() == null)
            {
                invocation.Proceed();
                return;
            }

            //��������ָ�������ԣ���ִ������
            try
            {
                PrintRequestInfo(invocation);
                invocation.Proceed();
                PrintResponseInfo(invocation);
            }
            catch (System.Exception ex)
            {
                //todo:��¼��־
                throw;
            }
        }

        /// <summary>
        /// ��ӡ��Ŀ�꺯����������Ϣ
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintRequestInfo(IInvocation invocation)
        {
            string paras = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
            PrintHelper.Print($"Calling method ��{invocation.Method.Name}�� with parameters ��{paras}��... ");
        }

        /// <summary>
        /// ��ӡĿ�꺯�����ؽ��
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintResponseInfo(IInvocation invocation)
        {
            PrintHelper.Print($"Done: result was ��{invocation.ReturnValue}��.");
        }
    }
}
