using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Shares;
using System.Reflection;

namespace AutofacCastleDynamicProxySample;

public class UnitTest5
{
    /// <summary>
    /// 基于自定义特性，实现对类内具体方法的拦截
    /// </summary>
    [Fact]
    public void Test1()
    {
        // create builder
        var builder = new ContainerBuilder();

        // 注册拦截器
        builder.RegisterType<CallLoggerMethodInterceptor>();

        // 注册接口及其实现类
        builder.RegisterType<RapService>()
            .As<IRapService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(CallLoggerMethodInterceptor));

        // 创建容器
        var container = builder.Build();
        // 解析服务
        var service = container.Resolve<IRapService>();

        // 执行
        service.Rap();
        service.Dance();//没用，不能打在interface上，因为拦截器里拿到的invocation是RapService
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
            PrintHelper.Print("托马斯回旋...");
        }
    }

    public class CallLoggerMethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            MethodInfo mi = invocation.MethodInvocationTarget
                            ?? invocation.Method;

            //如果目标函数没有添加指定的特性，则直接执行目标函数
            if (mi.GetCustomAttributes<CallLoggerAttribute>(true).FirstOrDefault() == null)
            {
                invocation.Proceed();
                return;
            }

            //如果添加了指定的特性，则执行拦截
            try
            {
                PrintRequestInfo(invocation);
                invocation.Proceed();
                PrintResponseInfo(invocation);
            }
            catch (System.Exception ex)
            {
                //todo:记录日志
                throw;
            }
        }

        /// <summary>
        /// 打印对目标函数的请求信息
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintRequestInfo(IInvocation invocation)
        {
            string paras = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
            PrintHelper.Print($"Calling method 【{invocation.Method.Name}】 with parameters 【{paras}】... ");
        }

        /// <summary>
        /// 打印目标函数返回结果
        /// </summary>
        /// <param name="invocation"></param>
        private void PrintResponseInfo(IInvocation invocation)
        {
            PrintHelper.Print($"Done: result was 【{invocation.ReturnValue}】.");
        }
    }
}
