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
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using RazorPagesMovie.Models;

namespace RazorPagesMovie
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
            services.AddDbContext<RazorPagesMovieContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazorPagesMovieContext")));

            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });

            services.AddRazorPages()
                .AddViewLocalization(opts => {
                opts.ResourcesPath = "Resources";
            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("lt"),
                    new CultureInfo("en")
                };
                opts.DefaultRequestCulture = new RequestCulture("lt");

                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;

                var cp = opts.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First(); // Culture provider
                cp.CookieName = "Culture";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddMvc()
                .AddRazorPagesOptions(options => {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizeFolder("/Account");
                    options.Conventions.AllowAnonymousToPage("/Account/Login");
                });
            services.AddScoped<IPortfolioService, PortfolioService>();
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
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
