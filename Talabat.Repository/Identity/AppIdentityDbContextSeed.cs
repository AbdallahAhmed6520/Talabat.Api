using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Abdallah",
                    Email = "abdallahAgmail.com",
                    UserName = "abdosedroho",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(User,"P@ssw0rd"); 
            }
        }
    }
}
