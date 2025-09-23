using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.CORE.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IELTSExamPlatform.DAL.Seed
{
    public static class ModelBuilderExtensions
    {
        public static async Task UseUserSeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }

            if (!userManager.Users.Any(x => x.NormalizedUserName == "ADMIN"))
            {
                var admin = new AppUser
                {
                    Id = IELTSExamPlatform.CORE.Helpers.IdGenerator.GenerateId(), 
                    Name = "Admin",
                    Surname = "Admin",
                    UserName = "Admin",
                    Email = "admin@gmail.com"
                };

                await userManager.CreateAsync(admin, "Admin123.");
                await userManager.AddToRoleAsync(admin, nameof(UserRole.Admin));
            }
        }
    }
}
