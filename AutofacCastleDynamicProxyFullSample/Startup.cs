using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using AutofacCastleDynamicProxyFullSample.AppServices;
using Castle.DynamicProxy;

namespace AutofacCastleDynamicProxyFullSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // In ASP.NET Core 3.x, using `Host.CreateDefaultBuilder` (as in the preceding Program.cs snippet) will
            // set up some configuration for you based on your appsettings.json and environment variables. See "Remarks" at
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder for details.
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called. Don't create a ContainerBuilder
            // for Autofac here, and don't call builder.Populate() - that
            // happens in the AutofacServiceProviderFactory for you.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<ProxyGenerator>();
            services.AddTransient<AppServiceInterceptor>();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

            //register adapter
            builder.RegisterGeneric(typeof(AsyncInterceptorAdaper<>));

            builder.RegisterAssemblyTypes(appServiceAssembly)
                .Where(cc => cc.Name.EndsWith("AppService"))//筛选具象类（concrete classes）
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces()//自动以其实现的所有接口类型暴露（包括IDisposable接口）
                .EnableInterfaceInterceptors()
                //.InterceptedBy(typeof(AppServiceInterceptor)) //异步拦截类，不能直接用
                .InterceptedBy(typeof(AsyncInterceptorAdaper<AppServiceInterceptor>))
                ;

            builder.RegisterAssemblyTypes(appServiceAssembly)
                .Where(cc => cc.Name.EndsWith("Validator"))
                .PublicOnly()
                .Where(cc => cc.IsClass)
                .AsImplementedInterfaces()
                ;
        }

        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(
          IApplicationBuilder app,
          IWebHostEnvironment env,
          ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
