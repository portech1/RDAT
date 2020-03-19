using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RDAT.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDAT.Models;
using RDAT.Services;

namespace RDAT
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<RDATContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RDATContext")));

            //services.AddIdentity<Models.User, IdentityRole>()
            //.AddEntityFrameworkStores<RDATContext>()
            //.AddDefaultTokenProviders(); // Remove to disable two factor authentication

            //

            services.AddDefaultIdentity<ApplicationUser>(o => o.User.AllowedUserNameCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+")
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RDATContext>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.ConfigureApplicationCookie(options => options.Cookie.IsEssential = true);

            services.AddTransient<RDAT.Services.IEmailSender, EmailSender>();

            // This was working
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<RDATContext>();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    // services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<RDATContext>()
            //    .AddDefaultTokenProviders();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            //    .AddRazorPagesOptions(options =>
            //    {
            //        options.AllowAreas = true;
            //        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            //        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            //    });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });



            services.AddControllersWithViews();
            services.AddMvc();
            //services.AddDbContext<RDATContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("RDATContext")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<RDATContext>();
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


            // who are you
            app.UseCookiePolicy();
            app.UseAuthentication();

            // are you allowed                     
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
