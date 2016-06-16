using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
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

        public ApplicationUserManager UserManager { get { return new ApplicationUserManager(new CustomUserStore(new ApplicationDbContext())); } }

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
        /// Kolekcja posiadanych uprawnień użytkownika.
        /// </summary>
        public List<AppRoles> userRoles { get; set; }

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
            value = reader.ReadElementString("ImieNazwisko");
            name = value != "" ? value : null;
            value = reader.ReadElementString("Adres");
            DBAdress = value != "" ? value : null;
            emaildn = reader.ReadElementString("Email");
            EmailConfirmed = reader.ReadElementString("PotwierdzonyEmail") == "True" ? true : false;
            value = reader.ReadElementString("NumerTelefonu");
            DBPhoneNumber = value != "" ? value : null;
            
            // Deserializacja uprawnień
            if (reader.Name == "Uprawnienia")
                userRoles = new List<AppRoles>(4);
            while (reader.Name == "Uprawnienia")
            {
                switch (reader.ReadElementString())
                {
                    case "Administrator":
                        userRoles.Add(AppRoles.Administrator);
                        break;
                    case "Patient":
                        userRoles.Add(AppRoles.Patient);
                        break;
                    case "Researcher":
                        userRoles.Add(AppRoles.Researcher);
                        break;
                    case "User":
                        userRoles.Add(AppRoles.User);
                        break;
                    default:
                        break;
                }
            }
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
            writer.WriteElementString("ImieNazwisko", name);
            writer.WriteElementString("Adres", DBAdress);
            writer.WriteElementString("Email", emaildn);
            writer.WriteElementString("PotwierdzonyEmail", EmailConfirmed.ToString());
            writer.WriteElementString("NumerTelefonu", DBPhoneNumber);

            // Serializacja uprawnień
            foreach (var item in UserManager.GetRoles(Id))
            {
                writer.WriteElementString("Uprawnienia", item.ToString());
            }
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
}