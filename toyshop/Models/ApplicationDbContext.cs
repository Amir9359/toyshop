using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using toyshop.Models;
using toyshop.Models.Groups;
using toyshop.Models.Payment;
using toyshop.Models.Products;
using toyshop.Models.Profile;

namespace toysite.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<PayItems> PayItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {

        }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var servicescope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<User> Usermanager = servicescope.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = servicescope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string username = configuration["Data:AdminUser:Name"];
                string email = configuration["Data:AdminUser:Email"];
                string password = configuration["Data:AdminUser:Password"];
                string role = configuration["Data:AdminUser:Role"];
                if (await Usermanager.FindByNameAsync(username) == null)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    var user = new User
                    {
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await Usermanager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await Usermanager.AddToRoleAsync(user, role);
                    }
                }
          
            }

        }
    }
}