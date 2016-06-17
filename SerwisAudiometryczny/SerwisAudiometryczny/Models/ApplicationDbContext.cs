using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Klasa kontekstu bazy danych
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        /// <summary>
        /// Metoda fabrykująca
        /// </summary>
        /// <returns>Instancja klasy kontekstu</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        /// <summary>
        /// Zbiór Audiogramów
        /// </summary>
        public DbSet<AudiogramModel> AudiogramModels { get; set; }
        /// <summary>
        /// Zbiór logów
        /// </summary>
        public DbSet<LogModel> LogModels { get; set; }
        /// <summary>
        /// Zbiór instrumentów
        /// </summary>
        public DbSet<InstrumentModel> InstrumentModels { get; set; }
        /// <summary>
        /// Zbiór częstotliwości
        /// </summary>
        public DbSet<FrequencyModel> FrequencyModels { get; set; }
    }
}
