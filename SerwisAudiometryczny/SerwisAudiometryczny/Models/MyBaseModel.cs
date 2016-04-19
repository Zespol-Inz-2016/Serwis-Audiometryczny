using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SerwisAudiometryczny.Interface;
using SerwisAudiometryczny.Models.Dicts;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerwisAudiometryczny.Models
{
    public abstract class MyBaseModel : IMyBase
    {
        public MyBaseModel()
        {
            CreationDate = DateTime.Now;
            Type = "MyBaseModel";
        }
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }
    public class MyBaseModelDBContext : DbContext
    {
        public MyBaseModelDBContext() : base("DefaultConnection")//Połaczenie z danym connectionstringiem
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        //public DbSet<MyBaseModel> MyBaseObjects { get; set; }
        public DbSet<DictSexModel> DictSexObjects { get; set; }
        public DbSet<DictDiagnosisModel> DictDiagnosisObjects { get; set; }
        public DbSet<DictInstrumentModel> DictInstrumentObjects { get; set; }
        public DbSet<DictNuisanceModel> DictNuisanceObjects { get; set; }
    }
}
