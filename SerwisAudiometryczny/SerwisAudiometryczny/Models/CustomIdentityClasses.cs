using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public enum AppRoles
    {
        Administrator, Patient, User, Researcher
    }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = Users.ToList().FirstOrDefault(u => u.DecryptedUserName == userName);
            return Task.FromResult<ApplicationUser>(user);
        }
        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = Users.ToList().FirstOrDefault(u => u.DecryptedEmail == email);
            return Task.FromResult<ApplicationUser>(user);
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
