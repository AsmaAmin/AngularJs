using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evidence_11CoreAPI_Angularjs.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evidence_11CoreAPI_Angularjs
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
             services.AddMvc();
            services.AddDbContext<DbGames>(o => o.UseSqlServer(Configuration.GetConnectionString("DC")));
            //services.AddCors();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DbGames>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(Options =>
            {
                Options.LoginPath = "";
                Options.Cookie.HttpOnly = true;
                Options.ExpireTimeSpan = TimeSpan.FromMinutes(6);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            CreateUserRole(services).Wait();
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});
            app.UseMvc();
        }

        public async Task CreateUserRole(IServiceProvider services)
        {

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (userManager.Users.Any())
            {
                return;
            }
            if (!roleManager.Roles.Any(r => r.Name == "admin"))
            {
                IdentityResult rr = roleManager.CreateAsync(new IdentityRole { Name = "admin" }).Result;

            }

            if(!userManager.Users.Any(u=>u.UserName == "a@a.com"))
            {
                var users = new IdentityUser
                {
                    UserName = "a@a.com",
                    Email = "a@a.com"
                };
                IdentityResult result = userManager.CreateAsync(users, "@Test123").Result;
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users, "admin");
                }
            }
        }
    }
}
