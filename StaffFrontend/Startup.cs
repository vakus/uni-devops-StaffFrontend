using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.AuthorizationProxy;
using StaffFrontend.Proxies.CustomerProxy;
using StaffFrontend.Proxies.ProductProxy;
using StaffFrontend.Proxies.RestockProxy;
using StaffFrontend.Proxies.ReviewProxy;

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
            services.AddHttpClient();

            services.AddAuthentication("Cookies").AddCookie("Cookies");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("StaffOnly", builder =>
                {
                    builder.RequireClaim("role", "Staff", "Manager"); //Manager has more access than Staff, but ultimately has access to all system the staff has
                });
                options.AddPolicy("ManagerOnly", builder =>
                {
                    builder.RequireClaim("role", "Manager");
                });
            });

            //Use preloading, HSTS for 360 days
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(360);
            });

            if (shouldRegisterFake("ProductMicroservice"))
            {
                services.AddSingleton<IProductProxy, ProductProxyLocal>();
            }
            else
            {
                services.AddHttpClient<IProductProxy, ProductProxyRemote>();
            }

            if (shouldRegisterFake("CustomerMicroservice:useFake"))
            {
                services.AddSingleton<ICustomerProxy, CustomerProxyLocal>();
            }
            else
            {
                services.AddHttpClient<ICustomerProxy, CustomerProxyRemote>();
            }

            if (shouldRegisterFake("ReviewMicroservice:useFake"))
            {
                services.AddSingleton<IReviewProxy, ReviewProxyLocal>();
            }
            else
            {
                services.AddHttpClient<IReviewProxy, ReviewProxyRemote>();
            }
            if (shouldRegisterFake("RestockMicroservice:useFake"))
            {
                services.AddSingleton<IRestockProxy, RestockProxyLocal>();
            }
            else
            {
                services.AddHttpClient<IRestockProxy, RestockProxyRemote>();
            }


            //Authorization Proxy doesnt have fake
            services.AddHttpClient<IAuthorizationProxy, AuthorizationProxyRemote>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpClientFactory factory)
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //warm up HttpClientFactory to prevent 10 minutes long first connection
            HttpClient client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            //any website is good as any
            client.GetAsync("https://google.com");
        }

        private bool shouldRegisterFake(string name)
        {
            return (_env.IsProduction() ? false : Configuration.GetValue<bool>(name + ":useFake"));
        }
    }
}