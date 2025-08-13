using CourseMarket.Shared.Services;
using CourseMarket.Web.Extensions;
using CourseMarket.Web.Handlers;
using CourseMarket.Web.Helpers;
using CourseMarket.Web.Models;
using CourseMarket.Web.Services;
using CourseMarket.Web.Services.Interfaces;
using CourseMarket.Web.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMarket.Web
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
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            services.AddHttpContextAccessor();
            services.AddOpenIdConnectAccessTokenManagement();
            services.AddSingleton<PhotoHelper>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddHttpClientServices(Configuration);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {
                opts.LoginPath = "/Auth/SignIn";
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                opts.SlidingExpiration = true;
                opts.Cookie.Name = "coursemarketcookie";
            });
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>());
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
            }
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
        }
    }
}
