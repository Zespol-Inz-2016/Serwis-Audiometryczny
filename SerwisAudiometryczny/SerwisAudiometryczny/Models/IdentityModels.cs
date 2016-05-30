using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SerwisAudiometryczny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IXmlSerializable
    {
        private ModelsDbContext dbContext = new ModelsDbContext();
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        
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

        public XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();

            Id = reader.ReadElementString("Id");
            UserName = reader.ReadElementString("NazwaUzytkownika");
            SecurityStamp = reader.ReadElementString("Zabezpieczenie");
            PasswordHash = reader.ReadElementString("Haslo");
            Administrator = reader.ReadElementString("Administrator") == "true" ? true : false;
            User = reader.ReadElementString("Uzytkownik") == "true" ? true : false;
            Researcher = reader.ReadElementString("Badacz") == "true" ? true : false;
            Patient = reader.ReadElementString("Pacjent") == "true" ? true : false;
            Name = reader.ReadElementString("ImieNazwisko");
            Address = reader.ReadElementString("Adres");
            Email = reader.ReadElementString("Email");
            PhoneNumber = reader.ReadElementString("NumerTelefonu");
            
            // TODO:
            /*
            while (reader.Name != "AudiogramId")
            {
                // TODO:
            } 

            while (reader.Name != "DostepWrazliweId")
            {
                // TODO:
            }
            */

            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            // Serializacja danych o koncie
            writer.WriteElementString("Id", Id);
            writer.WriteElementString("NazwaUzytkownika", UserName);
            writer.WriteElementString("Zabezpieczenie", SecurityStamp);
            writer.WriteElementString("Haslo", PasswordHash);
            writer.WriteElementString("Administrator", Administrator.ToString());
            writer.WriteElementString("Uzytkownik", User.ToString());
            writer.WriteElementString("Badacz", Researcher.ToString());
            writer.WriteElementString("Pacjent", Patient.ToString());
            writer.WriteElementString("ImieNazwisko", Name);
            writer.WriteElementString("Adres", Address);
            writer.WriteElementString("Email", Email);
            writer.WriteElementString("NumerTelefonu", PhoneNumber);

            // TODO: 
            /*
            // Serializacja audiogramów jeśli takowe istnieją (tylko Id)
            if (Audiograms != null)
            {
                foreach (AudiogramModel item in Audiograms)
                {
                    writer.WriteElementString("AudiogramId", item.ID.ToString());
                }
            }

            // Serializacja dostępu do danych wrażliwych jeśli takowe istnieją (tylko Id)
            if (SensitiveDataAccess != null)
            {
                foreach (ApplicationUser item in SensitiveDataAccess)
                {
                    writer.WriteElementString("DostepWrazliweId", item.Id);
                }
            } */
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