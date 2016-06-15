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
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Principal;

namespace SerwisAudiometryczny.Models
{
    public static class IdentityExtensions
    {
        public static string GetDecryptedUsername(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DecryptUserName");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// Klasa reprezentująca użytkownika.
    /// </summary>
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IXmlSerializable
    {
        public class AplicationUserConfig : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationUser>
        {
            public AplicationUserConfig()
            {
                Property(b => b.DBAdress);
                Property(b => b.DBName);
                Property(b => b.DBPhoneNumber);
            }
        }

        public UserManager<ApplicationUser, int> UserManager { get { return new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext())); } }

        private string name;
        /// <summary>
        /// Opisuje imię i nazwisko użytkownika.
        /// </summary>
        [NotMapped]
        [Display(Name = "Imię i Nazwisko")]
        public string Name
        {
            get
            {
                return Decrypt(name);
            }
            set
            {
                name = IsEncrypted(value) ? value : Encrypt(value);
            }
        }
        [Column("Name")]
        private string DBName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string address;
        /// <summary>
        /// Opisuje adres zamieszkania użytkownika.
        /// </summary>
        [NotMapped]
        [Display(Name = "Adres")]
        public string Address
        {
            get
            {
                return Decrypt(address);
            }
            set
            {
                address = IsEncrypted(value) ? value : Encrypt(value);
            }
        }
        [Column("Address")]
        private string DBAdress
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private string emaildn { get; set; }
        /// <summary>
        /// Opisuje adres e-mail użytkownika.
        /// </summary>
        [Display(Name = "Email")]
        public override string Email
        {
            get
            {
                return emaildn;
            }
            set
            {
                emaildn = IsEncrypted(value) ? value : Encrypt(value);
            }
        }
        /// <summary>
        /// Opisuje niezaszyfrowany adres e-mail użytkownika.
        /// </summary>
        [NotMapped]
        public string DecryptedEmail
        {
            get
            {
                return Decrypt(emaildn);
            }
            private set
            {
                emaildn = IsEncrypted(value) ? value : Encrypt(value);
            }
        }

        /// <summary>
        /// Opisuje numer telefonu użytkownika.
        /// </summary>
        [NotMapped]
        [Display(Name = "Numer telefonu")]
        public override string PhoneNumber
        {
            get
            {
                return Decrypt(base.PhoneNumber);
            }
            set
            {
                base.PhoneNumber = IsEncrypted(value) ? value : Encrypt(value);
            }
        }
        [Phone]
        [Column("PhoneNumber")]
        private string DBPhoneNumber
        {
            get
            {
                return base.PhoneNumber;
            }
            set
            {
                base.PhoneNumber = value;
            }
        }

        /// <summary>
        /// Opisuje nazwę użytkownika.
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
                base.UserName = IsEncrypted(value) ? value : Encrypt(value);
            }
        }
        /// <summary>
        /// Opisuje niezaszyfrowaną nazwę użytkownika.
        /// </summary>
        [NotMapped]
        public string DecryptedUserName
        {
            get
            {
                return Decrypt(base.UserName);
            }
            private set
            {
                base.UserName = Encrypt(value);
            }
        }

        /// <summary>
        /// Kolekcja zawierająca użytkowników, do których danych wrażliwych obecny szkodnik ma dostęp
        /// </summary>
        [Display(Name = "Dostęp do danych wrażliwych")]
        public ICollection<ApplicationUser> SensitiveDataAccess { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("DecryptUserName", DecryptedUserName));
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
            
            //Deserializacja danych o koncie 
            Id = int.Parse(reader.ReadElementString("Id"));
            base.UserName = reader.ReadElementString("NazwaUzytkownika");
            SecurityStamp = reader.ReadElementString("Zabezpieczenie");
            PasswordHash = reader.ReadElementString("Haslo");
            //Administrator = reader.ReadElementString("Administrator") == "True" ? true : false;
            //User = reader.ReadElementString("Uzytkownik") == "True" ? true : false;
            //Researcher = reader.ReadElementString("Badacz") == "True" ? true : false;
            //Patient = reader.ReadElementString("Pacjent") == "True" ? true : false;
            value = reader.ReadElementString("ImieNazwisko");
            name = value != "" ? value : null;
            value = reader.ReadElementString("Adres");
            DBAdress = value != "" ? value : null;
            emaildn = reader.ReadElementString("Email");
            EmailConfirmed = reader.ReadElementString("PotwierdzonyEmail") == "True" ? true : false;
            value = reader.ReadElementString("NumerTelefonu");
            DBPhoneNumber = value != "" ? value : null;

            // TODO: 
            /*
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
            writer.WriteElementString("NazwaUzytkownika", base.UserName);
            writer.WriteElementString("Zabezpieczenie", SecurityStamp);
            writer.WriteElementString("Haslo", PasswordHash);
            //writer.WriteElementString("Administrator", Administrator.ToString());
            //writer.WriteElementString("Uzytkownik", User.ToString());
            //writer.WriteElementString("Badacz", Researcher.ToString());
            //writer.WriteElementString("Pacjent", Patient.ToString());
            writer.WriteElementString("ImieNazwisko", name);
            writer.WriteElementString("Adres", DBAdress);
            writer.WriteElementString("Email", emaildn);
            writer.WriteElementString("PotwierdzonyEmail", EmailConfirmed.ToString());
            writer.WriteElementString("NumerTelefonu", DBPhoneNumber);

            // TODO:
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


        private string Encrypt(string clearText)
        {
            if (clearText == null || clearText == string.Empty)
                return clearText;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = "@" + Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            if (cipherText == null || cipherText == string.Empty)
                return cipherText;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText.Remove(0, 1));
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static bool IsEncrypted(string s)
        {
            if (s == null || s == string.Empty)
                return true;
            else
                return s.StartsWith("@");
        }
    }
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