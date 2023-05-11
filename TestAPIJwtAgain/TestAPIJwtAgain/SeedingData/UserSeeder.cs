using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using TestAPIJwtAgain.Model;

namespace TestAPIJwtAgain.SeedingData
{
    public  class UserSeeder 
    {


        public static async  Task StartAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
               await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if(!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
           
          
        }

     
    }
}
