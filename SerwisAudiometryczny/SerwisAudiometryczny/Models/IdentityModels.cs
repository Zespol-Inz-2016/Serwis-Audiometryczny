using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerwisAudiometryczny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Rola administratora.
        /// </summary>
        [Display(Name = "Administrator")]
        public bool Administrator { get; set; }
        /// <summary>
        /// Rola użytkownika
        /// </summary>
        [Display(Name = "Użytkownik")]
        public bool User { get; set; }
        /// <summary>
        /// Rola badacza
        /// </summary>
        [Display(Name = "Badacz")]
        public bool Researcher { get; set; }
        /// <summary>
        /// Rola pacjenta
        /// </summary>
        [Display(Name = "Pacjent")]
        public bool Patient { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public override string UserName
        {
            get
            {
                return base.UserName;
            }

            set
            {
                base.UserName = value;
            }
        }

        [Display(Name = "Audiogramy")]
        public ICollection<AudiogramModel> Audiograms { get; set; }

        [Display(Name = "Dostęp do danych wrażliwych")]
        public ICollection<ApplicationUser> SensitiveDataAccess { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}