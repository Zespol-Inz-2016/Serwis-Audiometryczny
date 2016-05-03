using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SerwisAudiometryczny.Interface;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerwisAudiometryczny.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public int ID { get; set; }
    }
    public class ModelsDbContext : DbContext
    {
        public ModelsDbContext() : base("DefaultConnection")//Połaczenie z danym connectionstringiem
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<AudiogramModel> AudiogramModels { get; set; }
        public DbSet<LogModel> LogModels { get; set; }
        public DbSet<InstrumentModel> InstrumentModels { get; set; }
        public DbSet<FrequencyModel> FrequencyModels { get; set; }
    }
}
