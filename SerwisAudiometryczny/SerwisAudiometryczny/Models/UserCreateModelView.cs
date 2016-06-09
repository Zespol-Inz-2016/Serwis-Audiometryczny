using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    public class UserCreateModelView
    {
        /// <summary>
        /// Opisuje imię użytkownika.
        /// </summary>
        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }

        /// <summary>
        /// Opisuje adres zamieszkania użytkownika.
        /// </summary>
        [Display(Name = "Adres")]
        public string Address { get; set; }

        /// <summary>
        /// /// Opisuje numer telefonu użytkownika.
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Opisuje adres mailowy użytkownika.
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Opisuje hasło dostępu do konta użytkownika.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        ///// <summary>
        ///// Opisuje typ użytkownika.
        ///// </summary>
        //public string Role { get; set; }

        /// <summary>
        /// Rola administratora.
        /// </summary>
        [Display(Name = "Administrator")]
        public bool Administrator { get; set; }

        /// <summary>
        /// Rola użytkownika
        /// </summary>
        [Display(Name = "Lekarz")]
        public bool User { get; set; }

        /// <summary>
        /// Rola pacjenta
        /// </summary>
        [Display(Name = "Pacjent")]
        public bool Patient { get; set; }

        /// <summary>
        /// Rola badacza
        /// </summary>
        [Display(Name = "Badacz")]
        public bool Researcher { get; set; }
    }
}
