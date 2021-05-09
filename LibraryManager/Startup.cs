using AutoMapper;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Mappers;
using LibraryManager.Validations.Interfaces;
using Manager;
using Manager.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;

namespace LibraryManager
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
            services.AddControllersWithViews();
            services.AddDbContext<LibraryManagerDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataBaseConnection")));

            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IEmployeeValidation, EmployeeValidation>();



            services.AddIdentity<Employee, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = true;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<LibraryManagerDBContext>()
            .AddDefaultTokenProviders();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ManagerMapper());
                mc.AddProfile(new ValidationMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Employee/LogIn");
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
        }
    }
}
