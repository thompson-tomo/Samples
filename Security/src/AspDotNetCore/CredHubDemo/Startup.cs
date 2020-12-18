﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.CloudFoundry;
using Steeltoe.Security.DataProtection.CredHub;
using Steeltoe.Security.DataProtection.CredHubCore;

namespace CredHubDemo
{
    public class Startup
    {
        ILoggerFactory logFactory;

        public Startup(IConfiguration configuration, ILoggerFactory logFactory)
        {
            Configuration = configuration;
            this.logFactory = logFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCloudFoundryActuators(Configuration);
            services.Configure<CredHubOptions>(Configuration.GetSection("CredHubClient"));
            services.AddCredHubClient(Configuration, logFactory);
#if NETCOREAPP3_1 || NET5_0
            services.AddControllersWithViews();
#else
            services.AddMvc();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCOREAPP3_1 || NET5_0
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCloudFoundryActuators();

#if NETCOREAPP3_1 || NET5_0
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
#else
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
#endif
        }
    }
}
