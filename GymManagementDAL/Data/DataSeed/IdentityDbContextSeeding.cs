

using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool seedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            try
            {   
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();
                if (HasUsers && HasRoles) return false;

                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new(){Name = "SuperAdmin"},
                        new(){Name = "Admin"},
                    };

                    foreach (var Role in Roles)
                    { 
                        if(!roleManager.RoleExistsAsync(Role.Name!).Result)
                            roleManager.CreateAsync(Role).Wait();
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Nour",
                        LastName = "Saber",
                        UserName = "NourSaber",
                        Email = "NourSaber@gmail.com",
                        PhoneNumber = "01010101010"
                    };
                    userManager.CreateAsync(MainAdmin,"P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Yasser",
                        LastName = "Mohamed",
                        UserName = "YasserMohamed",
                        Email = "YasserMohamed@gmail.com",
                        PhoneNumber = "01010101010"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();

                }
               
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }
    }
}
