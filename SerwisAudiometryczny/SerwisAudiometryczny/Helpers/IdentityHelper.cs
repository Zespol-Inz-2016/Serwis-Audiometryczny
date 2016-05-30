using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        internal static void SeedIdentities(DbContext context)
        {
            string userName = "admin@admin.com";
            string password = "Qwerty_123!";

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    Administrator = true
                };
                IdentityResult userResult = userManager.Create(user, password);

            }
        }
    }
}