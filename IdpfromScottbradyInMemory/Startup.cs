using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using System.Linq;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdpfromScottbrady
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //string connectionString = _configuration.GetConnectionString("IdentitySqlConnection");
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //services.AddDbContext<ApplicationDbContext>(builder =>
            //    builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly)));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>(); ;


            services.AddIdentityServer()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResurces())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddTestUsers(Config.GetTestUsers())
                .AddDeveloperSigningCredential();
                
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }


        //private static void InitializeDbTestData(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //    {
        //        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        //        serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
        //        serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

        //        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        //        if (!context.Clients.Any())
        //        {
        //            foreach (var client in Clients.Get())
        //            {
        //                context.Clients.Add(client.ToEntity());
        //            }
        //            context.SaveChanges();
        //        }

        //        if (!context.IdentityResources.Any())
        //        {
        //            foreach (var resource in Resources.GetIdentityResources())
        //            {
        //                context.IdentityResources.Add(resource.ToEntity());
        //            }
        //            context.SaveChanges();
        //        }

        //        if (!context.ApiScopes.Any())
        //        {
        //            foreach (var scope in Resources.GetApiScopes())
        //            {
        //                context.ApiScopes.Add(scope.ToEntity());
        //            }
        //            context.SaveChanges();
        //        }

        //        if (!context.ApiResources.Any())
        //        {
        //            foreach (var resource in Resources.GetApiResources())
        //            {
        //                context.ApiResources.Add(resource.ToEntity());
        //            }
        //            context.SaveChanges();
        //        }

        //        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        //        if (!userManager.Users.Any())
        //        {
        //            foreach (var testUser in Users.Get())
        //            {
        //                var identityUser = new IdentityUser(testUser.Username)
        //                {
        //                    Id = testUser.SubjectId
        //                };

        //                userManager.CreateAsync(identityUser, "Password123!").Wait();
        //                userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
        //            }
        //        }
        //    }
        //}

        //public void SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        //{
        //    if (!userManager.Users.Any())
        //    {
        //        var users = new List<IdentityUser>
        //        {
        //            new IdentityUser
        //            {
        //                UserName = "newman",
        //                Email = "test@test.com",
        //            },


        //        };

        //        foreach (var user in users)
        //        {
        //            userManager.CreateAsync(user, "Pa$$w0rd");
        //        }

        //        context.SaveChanges();
        //    }
        //}
    }
}
