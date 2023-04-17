using Castle.DynamicProxy;
using FluentValidation;

namespace ScrutorCastleDynamicProxyScanFullSample.AppServices
{
    public class AppServiceInterceptor : IAsyncInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public AppServiceInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);

            Console.WriteLine(((Task<TResult>)invocation.ReturnValue).Id);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 同步执行前");

            invocation.Proceed();

            Console.WriteLine($"{methodName} 同步执行完毕");
        }

        #region private

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 咳咳咳");
            await AutoValidation(invocation);
            invocation.Proceed();
            await (Task)invocation.ReturnValue;

            Console.WriteLine($"{methodName} 谢谢大家");
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            Console.WriteLine($"{methodName} 异步执行前");
            await AutoValidation(invocation);

            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;

            Console.WriteLine(task.Id);

            Console.WriteLine($"{methodName} 异步执行完毕");

            return result;
        }

        private async Task AutoValidation(IInvocation invocation)
        {
            var dto = invocation.Arguments.FirstOrDefault();//todo 多个参数情况
            var type = typeof(IValidator<>).MakeGenericType(dto.GetType());
            var validator= (IValidator)_serviceProvider.GetRequiredService(type);

            var validationContext = new ValidationContext<object>(dto);
            var validationResult = await validator.ValidateAsync(validationContext);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
        #endregion
    }
}
