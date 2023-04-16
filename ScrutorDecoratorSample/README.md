# 静态代理-Scrutor Decorate

开源：[https://github.com/khellang/Scrutor](https://github.com/khellang/Scrutor)

Scrutor处理大家熟悉的基于Ms DI扩展出的Scan扫描注册功能，还实现了Decoration（装饰者）模式。

当容器去拿ISpeakService服务时，拿到的就不是原本的SpeakService，而是MyDecorator，而MyDecorator里持有原本的SpeakService。

实现原理是，Decorate会修改原本的ServiceDescriptor，将本来的构造方式改为Factory模式，利用Factory返回MyDecorator实例。

```
            builder.Services.AddScoped<ISpeakService, SpeakService>();
            // 此时 ServiceType: Shares.ISpeakService Lifetime: Scoped ImplementationType: Shares.SpeakService

            builder.Services.Decorate<ISpeakService, MyDecorator>();
            // 此时 ServiceType: Shares.ISpeakService Lifetime: Scoped ImplementationFactory: System.Object <Decorate>b__0(System.IServiceProvider)
```

其中，Factory构造Decorator对象，Scrutor封装了GetInstance方法，利用了老的ServiceDescriptor，调用Ms DI的ActivatorUtilities.CreateInstance(provider, type, arguments)方法来构造。

```
        private static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
            }

            return descriptor.ImplementationFactory(provider);
        }

        private static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
        }

        private static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(provider, type, arguments);
        }
```

arguments传入了原本的Instance实例，即SpeakService实例。

所以当构造MyDecorator时，如果发现它的构造函数依赖了ISpeakService，则会使用arguments指定的SpeakService实例，从而避免了循环依赖。