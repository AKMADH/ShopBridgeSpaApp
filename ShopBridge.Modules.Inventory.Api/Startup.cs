using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ShopBridge.Infrastructure.Utils.Configuration;
using ShopBridge.Infrastructure.Utils.Configuration.Context;
using ShopBridge.Infrastructure.Utils.Configuration.IContext;
using ShopBridge.Modules.Inventory.Logic.Registry;
using System;
using System.IO;

namespace ShopBridge.Modules.Inventory.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string filename = Directory.GetCurrentDirectory() + "\\Log\\" + DateTime.UtcNow.ToString("ddMMyyyy") + "\\shopbridge.txt";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(filename)
                .CreateLogger();
            services.AddControllers();
            services.AddScoped<IDBContext, DBContext>();
            services.AddMvcCore().AddApiExplorer();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IDBContext>(service =>
            {
                var provider = service.GetRequiredService<ITenantProvider>();
                return provider.GetTenant();
            });


            services.AddScoped(service =>
            {
                var provider = service.GetRequiredService<ITenantProvider>();
                return provider.GetTenant();
            });
            LogicRegistry.IncludeLogicRegistry(services);

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
