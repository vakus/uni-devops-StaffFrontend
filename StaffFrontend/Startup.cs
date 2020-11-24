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
using StaffFrontend.Proxies;

namespace StaffFrontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            //Use preloading, HSTS for 360 days
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(360);
            });

            if (_env.IsDevelopment())
            {
                services.AddSingleton<IProductProxy, ProductProxyLocal>();
                services.AddSingleton<ICustomerProxy, CustomerProxyLocal>();
                services.AddSingleton<IReviewProxy, ReviewProxyLocal>();
            }
            else if (_env.IsStaging())
            {
                services.AddSingleton<IProductProxy, ProductProxyRemote>();
                services.AddSingleton<ICustomerProxy, CustomerProxyRemote>();
                services.AddSingleton<IReviewProxy, ReviewProxyRemote>();
            }
            else
            {
                services.AddSingleton<IProductProxy, ProductProxyRemote>();
                services.AddSingleton<ICustomerProxy, CustomerProxyRemote>();
                services.AddSingleton<IReviewProxy, ReviewProxyRemote>();
            }
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
