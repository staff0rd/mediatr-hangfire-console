using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.Console;
using MediatR;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using mediatr_hangfire_console.Controllers;

namespace mediatr_hangfire_console
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire((provider, config) => 
            {
                config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"));
                config.UseConsole();
                config.UseSerializerSettings(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
            });

            services.AddSingleton<JobActivator, InjectContextJobActivator>();

            services.AddHangfireServer();

            services.AddMediatR(GetType());

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            factory.AddProvider(new HangfireConsoleLoggerProvider());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHangfireDashboard();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
