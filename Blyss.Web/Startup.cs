namespace Blyss.Web
{

    using System;
    using AutoMapper;
    using Data;
    using Entities;
    using Helpers;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });

            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            services.Configure<SendGridDetails>(this.Configuration.GetSection("SendGridDetails"));

            services.AddAutoMapper();

            services.ConfigureApplicationCookie(options => { options.ExpireTimeSpan = TimeSpan.FromDays(7); });

            services.AddDbContext<BlyssContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("Default"));
            });


            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User = new UserOptions
                    {
                        RequireUniqueEmail = true
                    };

                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 6,
                        RequireNonAlphanumeric = false,
                        RequiredUniqueChars = 1,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireDigit = true
                    };

                    options.SignIn = new SignInOptions
                    {
                        RequireConfirmedEmail = true
                    };
                })
                .AddEntityFrameworkStores<BlyssContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Error");
//                app.UseStatusCodePagesWithReExecute("/Error/{0}");
//            }

            app.SeedData();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseMvc(options =>
            {
                options.MapRoute(
                    "area",
                    "{area:exists}/{controller}/{action}");

                options.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }

}