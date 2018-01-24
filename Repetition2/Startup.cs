using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repetition2.Data;
using Repetition2.Models;
using Repetition2.Services;
using Repetition2.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Repetition2.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Repetition2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

			// DETTA ÄR TILLLAGD
			ITimeProvider myFakeTimeProvider = new FakeTimeProvider();
			myFakeTimeProvider.Now = new DateTime(2018, 2, 2);
			services.AddSingleton<ITimeProvider>(myFakeTimeProvider);
			// för realtime adda (new RealTimeProvider) istället för myFakeTimeProvider
			//UNTIL HERE

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddLocalization();
            services.AddMvc()
			 .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
					opts => { opts.ResourcesPath = "Resources"; })
				.AddDataAnnotationsLocalization(options =>
				{
					options.DataAnnotationLocalizerProvider = (type, factory) =>
						factory.Create(typeof(SharedResources));
				});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
			, ApplicationDbContext context, UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
        {

			var supportedCultures = new[]
			{
				new CultureInfo("en-US"),
				new CultureInfo("sv-SE")
			};

			var options = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("SV-SE"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			};

			options.RequestCultureProviders = new[]
			{
				new CookieRequestCultureProvider() { Options = options }
			};

			app.UseRequestLocalization(options);

			if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });

			DbSeeder.Seeder(context,userManager,roleManager);
		}
    }
}
