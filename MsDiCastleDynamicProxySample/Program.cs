using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shares;

namespace MsDiDynamicProxySample
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

            builder.Services.TryAddSingleton<ProxyGenerator>();
            builder.Services.AddTransient<MyInterceptor>();

            builder.Services.AddScoped<SpeakService>();
            builder.Services.AddScoped<ISpeakService>(sp=> 
                sp.GetRequiredService<ProxyGenerator>()
                    .CreateInterfaceProxyWithTargetInterface<ISpeakService>(sp.GetRequiredService<SpeakService>(),sp.GetRequiredService<MyInterceptor>())
                );

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