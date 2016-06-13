using System.Data.Entity;
using SerwisAudiometryczny.Interface;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Bazowy model po którym dziedziczą inne modele.
    /// </summary>
    public abstract class BaseModel : IBaseModel
    {
        /// <summary>
        /// Id obiektu
        /// </summary>
        public int ID { get; set; }
    }

    /// <summary>
    /// Baza zawierająca wszystkie modele. Dziedziczy po DbContext.
    /// </summary>
    public class ModelsDbContext : DbContext
    {
        /// <summary>
        /// Kontruktor
        /// </summary>
        public ModelsDbContext() : base("DefaultConnection")//Połaczenie z danym connectionstringiem
        {
        }

        /// <summary>
        /// Kolekcja wszystkich audiogramów.
        /// </summary>
        public DbSet<AudiogramModel> AudiogramModels { get; set; }

        /// <summary>
        /// Kolekcja wszystkich logów.
        /// </summary>
        public DbSet<LogModel> LogModels { get; set; }

        /// <summary>
        /// Kolekcja wszystkich instrumentów.
        /// </summary>
        public DbSet<InstrumentModel> InstrumentModels { get; set; }

        /// <summary>
        /// Kolekcja wszystkich częstotliwości.
        /// </summary>
        public DbSet<FrequencyModel> FrequencyModels { get; set; }
    }
}
