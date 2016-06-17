using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Helpers;

[assembly: OwinStartupAttribute(typeof(SerwisAudiometryczny.Startup))]
namespace SerwisAudiometryczny
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        /// <summary>
        /// Metoda tworząca role w bazie.
        /// </summary>
        public static void CreateRolesAndUsers()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(dbContext));
            var userManager = new UserManager<ApplicationUser, int>(new CustomUserStore(dbContext));

            if (!roleManager.RoleExists("Administrator"))
            {                           
                var role = new CustomRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                int id = 1;
                ApplicationUser user = userManager.FindById(1);
                userManager.AddToRole(user.Id, AppRoles.Administrator.ToString());
            }
 
            if (!roleManager.RoleExists("Patient"))
            {
                var role = new CustomRole();
                role.Name = "Patient";
                roleManager.Create(role);
            }
 
            if (!roleManager.RoleExists("Researcher"))
            {
                var role = new CustomRole();
                role.Name = "Researcher";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new CustomRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            dbContext.SaveChanges();
        }
    }
}