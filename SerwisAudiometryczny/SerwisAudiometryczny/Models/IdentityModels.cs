﻿using System.Security.Claims;
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
using System.Linq;

namespace SerwisAudiometryczny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// <summary>
    /// Klasa reprezentująca użytkownika.
    /// </summary>
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IXmlSerializable
    {
        [NotMapped]
        public DecryptedUser Decrypted { get; set; }
        [NotMapped]
        public virtual ApplicationUserManager UserManager { get; set; }

        public ApplicationUser(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            Decrypted = new DecryptedUser(this);
        }

        public ApplicationUser()
            : this(new ApplicationUserManager(new CustomUserStore(new ApplicationDbContext())))
        {
        }

        private ApplicationUser(bool asNested)
        { }
        [NotMapped]
        public class DecryptedUser : ApplicationUser
        {
            private ApplicationUser _parent;

            internal DecryptedUser(ApplicationUser parent)
                : base(true)
            {
                _parent = parent;
            }
            [Display(Name = "Email")]
            public override string Email
            {
                get
                {
                    return Encoder.IsEncrypted(_parent.Email) ? Encoder.Decrypt(_parent.Email) : _parent.Email;
                }

                set
                {
                    _parent.Email = Encoder.IsEncrypted(value) ? Encoder.Encrypt(value) : value;
                }
            }
            public override int Id
            {
                get
                {
                    return _parent.Id;
                }

                set
                {
                    _parent.Id = value;
                }
            }
            [Display(Name = "Numer telefonu")]
            public override string PhoneNumber
            {
                get
                {
                    return _parent.PhoneNumber;
                }

                set
                {
                    _parent.PhoneNumber = value;
                }
            }
            [Display(Name = "Adres")]
            public override string Address
            {
                get
                {
                    return Encoder.IsEncrypted(_parent.Address) ? Encoder.Decrypt(_parent.Address) : _parent.Address;
                }

                set
                {
                    _parent.Address = Encoder.IsEncrypted(value) ? Encoder.Encrypt(value) : value;
                }
            }
            [Display(Name = "Imię i Nazwisko")]
            public override string Name
            {
                get
                {
                    return Encoder.IsEncrypted(_parent.Name) ? Encoder.Decrypt(_parent.Name) : _parent.Name;
                }

                set
                {
                    _parent.Name = Encoder.IsEncrypted(value) ? Encoder.Encrypt(value) : value;
                }
            }
            public override string UserName
            {
                get
                {
                    return _parent.UserName;
                }

                set
                {
                    _parent.UserName = value;
                }
            }
            public override ApplicationUserManager UserManager
            {
                get
                {
                    return _parent.UserManager;
                }

                set
                {
                    _parent.UserManager = value;
                }
            }
        }

        /// <summary>
        /// Opisuje imię i nazwisko użytkownika.
        /// </summary>
        [Display(Name = "Imię i Nazwisko")]
        public virtual string Name { get; set; }
        /// <summary>
        /// Opisuje adres zamieszkania użytkownika.
        /// </summary>
        [Display(Name = "Adres")]
        public virtual string Address { get; set; }
        /// <summary>
        /// Opisuje adres e-mail użytkownika.
        /// </summary>
        [Display(Name = "Email")]
        public override string Email { get; set; }
        /// <summary>
        /// Opisuje numer telefonu użytkownika.
        /// </summary>
        [Display(Name = "Numer telefonu")]
        public override string PhoneNumber { get; set; }
        /// <summary>
        /// Opisuje nazwę użytkownika.
        /// </summary>

        /// <summary>
        /// Kolekcja posiadanych uprawnień użytkownika.
        /// </summary>
        public virtual List<AppRoles> userRoles { get; set; }
        public virtual string SensitiveDataAccessStorage { get; set; }
        /// <summary>
        /// Kolekcja zawierająca użytkowników, do których danych wrażliwych obecny szkodnik ma dostęp
        /// </summary>
        [Display(Name = "Dostęp do danych wrażliwych")]
        public virtual int[] SensitiveDataAccessIds
        {
            get
            {
                return Array.ConvertAll(SensitiveDataAccessStorage.Split(';'), int.Parse);
            }
            set
            {
                SensitiveDataAccessStorage = string.Join(";", value.OrderBy(x=>x).Distinct().ToArray());
            }
        }
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

            //Deserializacja danych o koncie 
            Id = int.Parse(reader.ReadElementString("Id"));
            base.UserName = reader.ReadElementString("NazwaUzytkownika");
            SecurityStamp = reader.ReadElementString("Zabezpieczenie");
            PasswordHash = reader.ReadElementString("Haslo");
            value = reader.ReadElementString("ImieNazwisko");
            Name = value != "" ? value : null;
            value = reader.ReadElementString("Adres");
            Address = value != "" ? value : null;
            Email = reader.ReadElementString("Email");
            EmailConfirmed = reader.ReadElementString("PotwierdzonyEmail") == "True" ? true : false;
            value = reader.ReadElementString("NumerTelefonu");
            PhoneNumber = value != "" ? value : null;

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
            writer.WriteElementString("ImieNazwisko", Name);
            writer.WriteElementString("Adres", Address);
            writer.WriteElementString("Email", Email);
            writer.WriteElementString("PotwierdzonyEmail", EmailConfirmed.ToString());
            writer.WriteElementString("NumerTelefonu", PhoneNumber);

            // Serializacja uprawnień
            foreach (var item in UserManager.GetRoles(Id))
            {
                writer.WriteElementString("Uprawnienia", item.ToString());
            }
        }
    }
}