using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Shares;

namespace ScrutorDecoratorScanSample
{
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

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<IAppService>()
                .AddClasses(classes => classes.AssignableTo<IAppService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            builder.Services.AddSingleton<ProxyGenerator>();
            builder.Services.AddTransient<MyInterceptor>();

            List<ServiceDescriptor> serviceDescriptorList = builder.Services.Where(r => r.ServiceType.IsAssignableTo(typeof(IAppService))).ToList();
            foreach (var registration in serviceDescriptorList)
            {
                builder.Services.AddScoped(registration.ServiceType, sp =>
                {
                    var generator = sp.GetRequiredService<ProxyGenerator>();
                    var interceptor = sp.GetRequiredService<MyInterceptor>();
                    var target = sp.GetRequiredService(registration.ServiceType);//todo

                    return generator.CreateInterfaceProxyWithTargetInterface(
                            registration.ServiceType,
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
    }
}