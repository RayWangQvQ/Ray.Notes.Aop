using Castle.DynamicProxy;
using Shares;

namespace ScrutorCastleDynamicProxySample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<IAppService>()
                .AddClasses(classes => classes.AssignableTo<IAppService>())
                .AsImplementedInterfaces()
                //.AsSelf()
                .WithTransientLifetime());

            builder.Services.AddSingleton<ProxyGenerator>();
            builder.Services.AddTransient<MyInterceptor>();

            List<ServiceDescriptor> serviceDescriptorList = builder.Services
                .Where(r => r.ServiceType.IsAssignableTo(typeof(IAppService)) && r.ServiceType.IsInterface)
                .ToList();
            foreach (var descriptor in serviceDescriptorList)
            {
                builder.Services.AddScoped(descriptor.ServiceType, sp =>
                {
                    var generator = sp.GetRequiredService<ProxyGenerator>();
                    var interceptor = sp.GetRequiredService<MyInterceptor>();

                    //var target = sp.GetRequiredService(registration.ServiceType!);//循环依赖，报错
                    //var target = sp.GetRequiredService(registration.ImplementationType!);//需要AsSelf()注册本身
                    var target = sp.GetInstance(descriptor);//指定ServiceDescriptor

                    return generator.CreateInterfaceProxyWithTargetInterface(
                            descriptor.ServiceType,
                            target,
                            interceptor);
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

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
    }
}