﻿using Castle.DynamicProxy;

namespace MsDiDynamicProxySample
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("咳咳咳");
            invocation.Proceed();
            Console.WriteLine("谢谢大家");
        }
    }
}
