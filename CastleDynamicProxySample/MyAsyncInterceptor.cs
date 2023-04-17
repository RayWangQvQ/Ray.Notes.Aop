using Castle.DynamicProxy;

namespace CastleDynamicProxySample
{
    public class MyAsyncInterceptor : IAsyncInterceptor
    {
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 咳咳咳");

            invocation.Proceed();
            await (Task)invocation.ReturnValue;

            Console.WriteLine($"{methodName} 谢谢大家");
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);

            Console.WriteLine(((Task<TResult>)invocation.ReturnValue).Id);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 异步执行前");

            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;

            Console.WriteLine(task.Id);

            Console.WriteLine($"{methodName} 异步执行完毕");

            return result;
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 同步执行前");

            invocation.Proceed();

            Console.WriteLine($"{methodName} 同步执行完毕");
        }
    }
}
