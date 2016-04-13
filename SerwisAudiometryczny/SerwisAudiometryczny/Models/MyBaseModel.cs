using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    public class MyBaseModel
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
        public DbSet<MyBaseModel> MyBaseObjects { get; set; }
    }
}
