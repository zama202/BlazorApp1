using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp1.Data;
using BlazorStrap;
using BlazorApp1.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Azure.SignalR;

namespace BlazorApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //AZURE SIGNALR SERVICE
            //bisogna installare nuget Microsoft.Azure.SignalR
            //services.AddSignalR().AddAzureSignalR(config =>
            //{
            //    config.ServerStickyMode = ServerStickyMode.Required;
            //    config.ConnectionString = "Endpoint=https://demobrainkin.service.signalr.net;AccessKey=1UymyA/+halds47Vk7BxiF62dcSf5prARsjY0hIBqdk=;Version=1.0;";
            //});


            //InLOCALE
            services.AddSignalR();
            
            services.AddControllersWithViews();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBootstrapCss();
            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<AlertService>();
            services.AddSingleton<NotificationService>();

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

            app.UseRouting();

            //AZURE SIGNALR SERVICE
            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<NotificationHub>("/NotificationHub");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<NotificationHub>("/NotificationHub");
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

        }
    }
}
