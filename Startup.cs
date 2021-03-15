using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnTheLaneOfHike.Models;
using OnTheLaneOfHike.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike
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
            services.AddTransient<IAppRepository, AppRepository>(); // repository Interface then repo class
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IProposalRepository, ProposalRepository>();
            services.AddControllersWithViews();
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DataBaseContext")));
            // Stuff added for Identity
            services.AddIdentity<MemberModel, IdentityRole>()
             .AddEntityFrameworkStores<DataBaseContext>()
             .AddDefaultTokenProviders();
            //End identity stuff
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
            app.Use(async (ctx, next) => {  //for header not set error
                ctx.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            // cache control
        

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
        
           // var serviceProvider = app.ApplicationServices;
           // var userManager = serviceProvider.GetRequiredService<UserManager<MemberModel>>();
          // var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedData.CreateAdminUser(app.ApplicationServices).Wait();
        }
    }
}
