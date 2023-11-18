using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using Web.Mvc.Helper;
using Web.Mvc.Models;

namespace Web.Mvc
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Info("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                var emailConfig = builder.Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
                builder.Services.AddSingleton(emailConfig);

                //builder.Services.AddControllers();

                // Add services to the container.
                builder.Services.AddControllersWithViews();
                // Hook up NLog
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                var app = builder.Build();



                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {

                // NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }

            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }
    }
}