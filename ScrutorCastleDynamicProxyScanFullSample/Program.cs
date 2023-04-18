using Castle.DynamicProxy;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ScrutorCastleDynamicProxyScanFullSample.AppServices;

namespace ScrutorCastleDynamicProxyScanFullSample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAppServices()
            .AddValidation();

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
}

public static class MyExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        //AppService
        services.Scan(scan =>
            scan.FromAssemblyOf<Program>()
                .AddClasses(classes => classes.AssignableTo<IAppService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        //AOP
        services.AddSingleton<ProxyGenerator>();
        services.AddTransient<AppServiceInterceptor>();
        List<ServiceDescriptor> serviceDescriptorList = services
            .Where(r => r.ServiceType.IsAssignableTo(typeof(IAppService)) && r.ServiceType.IsInterface)
            .ToList();
        foreach (var descriptor in serviceDescriptorList)
        {
            services.AddScoped(descriptor.ServiceType, sp =>
            {
                var generator = sp.GetRequiredService<ProxyGenerator>();
                var interceptor = sp.GetRequiredService<AppServiceInterceptor>();

                var target = sp.GetInstance(descriptor);//Ö¸¶¨ServiceDescriptor

                return generator.CreateInterfaceProxyWithTargetInterface(
                    descriptor.ServiceType,
                    target,
                    interceptor);
            });
        }

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(scan =>
            scan.FromAssemblyOf<Program>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        return serviceCollection;
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
}
