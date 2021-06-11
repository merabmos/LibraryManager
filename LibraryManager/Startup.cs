using AutoMapper;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Managers.Main;
using LibraryManager.Mappers;
using LibraryManager.Models.BooksShelfModels;
using LibraryManager.Validations;
using LibraryManager.Validations.Interfaces;
using Manager.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<LibraryManagerDBContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("DataBaseConnection")));

            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IEmployeeValidation, EmployeeValidation>();
            services.AddScoped<IAdministrationManager,AdministrationManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IFilter<>), typeof(Filter<>));
            services.AddScoped(typeof(IExpressionTree<>), typeof(ExpressionTree<>));
            
            
            services.AddScoped<AuthorManager>();          
            services.AddScoped<GenreManager>();          
            services.AddScoped<SectorManager>();
            services.AddScoped<SectorManager>();
            services.AddScoped<SectionManager>();
            services.AddScoped<BooksShelfManager>();
            services.AddScoped<BookManager>();


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
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
               // app.UseExceptionHandler("/Home/Error");
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
