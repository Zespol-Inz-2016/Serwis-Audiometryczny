using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SerwisAudiometryczny.Controllers;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Helpers
{
    public class IdentityHelper
    {

        internal static void SeedIdentities(ApplicationDbContext context)
        {
            int Id = 1;
            string password = "Qwerty_123!";

            var userManager = new ApplicationUserManager(new CustomUserStore(context));
            userManager.UserValidator = new CustomValidator(userManager) { AllowOnlyAlphanumericUserNames = false };

            ApplicationUser user = userManager.FindById(Id);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };
                user.Decrypted.Email = "admin@admin.com";
                user.Decrypted.Address = "tam gdzie admin miszka";
                user.Decrypted.Name = "Janusz Admin";

                IdentityResult userResult = userManager.Create(user, password);
            }
        }
    }
}