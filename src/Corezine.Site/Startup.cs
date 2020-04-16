using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corezine.Domain.Contracts;
using Corezine.Domain.Data;
using Corezine.Domain.Models;
using Corezine.Domain.Repositories;
using Corezine.Site.Areas.Dashboard.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Corezine.Services;

namespace Corezine.Site
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
            services.AddDbContext<CorezineDatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultSqlServer"));
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<CorezineDatabaseContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
            });
            services.AddSession();
            services.AddFeedback();
            
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<DashboardModel, DashboardModel>();
            services.AddControllersWithViews();
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
            app.UseSession();
            app.UseFeedback();
            app.UseEndpoints(endpoints =>
            {
                //FrontEnd Area
                endpoints.MapAreaControllerRoute(
                    name : "Users Frontend",
                    areaName : "Frontend",
                    pattern : "{controller=Home}/{action=Index}/{id?}"
                );
                //Dashboard Area
                endpoints.MapAreaControllerRoute(
                    name : "Dashboard",
                    areaName : "Dashboard",
                    pattern : "Dashboard/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
