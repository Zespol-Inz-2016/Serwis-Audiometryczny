using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SerwisAudiometryczny.Models;

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
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));
            var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
  
            if (!roleManager.RoleExists("Administrator"))
            {
                               
                var role = new CustomRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                string UserName = "admin@admin.com";
                ApplicationUser user = UserManager.FindByName(UserName);

                UserManager.AddToRole(user.Id, "Administrator");

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
        }
    }
}