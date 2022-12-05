//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Threading.Tasks;

//namespace Identity.Models
//{
//    public class ContextSeed
//    {
//        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            //Seed Roles
//            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
//            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
//            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Manager.ToString()));
//            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
//        }

//        internal static Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
