using AutoMapper;
using Business.Helpers.Mapping.MyAutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.ActionFilters;
using UIWeb.HttpContextHelpers;

namespace UIWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<VisitingAF>();

            services.AddTransient<ISessionService, SessionManager>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MainProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(VisitingAF));
            })
               .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(86400);
            });

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSession();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("default");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
