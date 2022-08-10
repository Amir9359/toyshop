using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Groups;
using toyshop.Models.Payment;
using toyshop.Models.Products;
using toyshop.Models.Profile;
using toyshop.Repository;
using toysite.Models;

namespace toyshop
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

            services.AddDbContext<ApplicationDbContext>(option =>
                    option.UseSqlServer(Configuration["Data:toyshop:ConnectionStrings"]));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IProductRepository , ProductRepository>();
            services.AddScoped<IProductItemRepository , ProductItemRepository>();
            services.AddScoped<IPayItemRepository , PayItemRepository>();
            services.AddScoped<IPayRepository , PayRopository>();
            services.AddScoped<IAddressRepository , AddressRepository>();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Admin/Account/Signin";
            });
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();

            app.UseMvc(route =>
            {
                route.MapRoute(name: "MyArea", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                route.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");

            });
             ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
