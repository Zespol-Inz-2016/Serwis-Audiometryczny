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
using System.Linq;

namespace SerwisAudiometryczny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IXmlSerializable
    {
        // private ModelsDbContext dbContext;
        // private ApplicationDbContext applicationDbContext;

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

        /// <summary>
        /// Imie i nazwisko
        /// </summary>
        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }

        /// <summary>
        /// Adres użytkownika
        /// </summary>
        [Display(Name = "Adres")]
        public string Address { get; set; }

        /// <summary>
        /// Email użytkownika
        /// </summary>
        [Display(Name = "Email")]
        public override string Email { get; set; }

        /// <summary>
        /// Rola pacjenta
        /// </summary>
        [Display(Name = "Numer telefonu")]
        public override string PhoneNumber { get; set; }

        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
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
        /// <summary>
        /// Kolekcja audiogramów przypisanych do użytkownika
        /// </summary>
        [Display(Name = "Audiogramy")]
        public ICollection<AudiogramModel> Audiograms { get; set; }

        /// <summary>
        /// Kolekcja dostępu do danych wrażliwych innych użytkowników
        /// </summary>
        [Display(Name = "Dostęp do danych wrażliwych")]
        public ICollection<ApplicationUser> SensitiveDataAccess { get; set; }

        /// <summary>
        /// Tworzenie naszego cookie
        /// </summary>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Wymagana przez interfejs IXmlSerializable, ale nie zalecana 
        /// w impelementacji własnej.
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return (null);
        }

        /// <summary>
        /// Generuje obiekt z jego reprezentacji XML.
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            string value = "";
            reader.MoveToContent();
            reader.ReadStartElement();

            Id = int.Parse(reader.ReadElementString("Id"));
            UserName = reader.ReadElementString("NazwaUzytkownika");
            SecurityStamp = reader.ReadElementString("Zabezpieczenie");
            PasswordHash = reader.ReadElementString("Haslo");
            Administrator = reader.ReadElementString("Administrator") == "True" ? true : false;
            User = reader.ReadElementString("Uzytkownik") == "True" ? true : false;
            Researcher = reader.ReadElementString("Badacz") == "True" ? true : false;
            Patient = reader.ReadElementString("Pacjent") == "True" ? true : false;
                value = reader.ReadElementString("ImieNazwisko");
            Name = value != "" ? value : null;
                value = reader.ReadElementString("Adres");
            Address = value != "" ? value : null;
            Email = reader.ReadElementString("Email");
            EmailConfirmed = reader.ReadElementString("PotwierdzonyEmail") == "True" ? true : false;
                value = reader.ReadElementString("NumerTelefonu");
            PhoneNumber = value != "" ? value : null;

            // TODO: 
            /*
            if (reader.Name == "AudiogramId")
                Audiograms = new List<AudiogramModel>();
            while (reader.Name == "AudiogramId")
            {
                int audiogramId = int.Parse(reader.ReadElementString());
                AudiogramModel x = dbContext.AudiogramModels.ToList().Find(item => item.ID == audiogramId);
                Audiograms.Add(x);
            } 
            while (reader.Name == "DostepWrazliweId")
            {
                // TODO:
            }
            */

            reader.Read();
        }

        /// <summary>
        /// Konwertuje obiekt na jego reprezentację XML.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            // Serializacja danych o koncie
            writer.WriteElementString("Id", Id.ToString());
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
            writer.WriteElementString("PotwierdzonyEmail", EmailConfirmed.ToString());
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

            /*
            // Serializacja dostępu do danych wrażliwych jeśli takowe istnieją (tylko Id)
            if (SensitiveDataAccess != null)
            {
                foreach (var item in SensitiveDataAccess)
                {
                    writer.WriteElementString("DostepWrazliweId", item.Id);
                }
            } 
            */
        }
    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

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
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

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
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<AudiogramModel> AudiogramModels { get; set; }
        public DbSet<LogModel> LogModels { get; set; }
        public DbSet<InstrumentModel> InstrumentModels { get; set; }
        public DbSet<FrequencyModel> FrequencyModels { get; set; }
    }
}