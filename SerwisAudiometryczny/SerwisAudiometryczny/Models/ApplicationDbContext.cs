using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUser.AplicationUserConfig());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AudiogramModel> AudiogramModels { get; set; }
        public DbSet<LogModel> LogModels { get; set; }
        public DbSet<InstrumentModel> InstrumentModels { get; set; }
        public DbSet<FrequencyModel> FrequencyModels { get; set; }
    }
}
