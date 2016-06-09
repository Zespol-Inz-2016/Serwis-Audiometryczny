using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    public class AccountEditViewModel
    {
        /// <summary>
        /// Opisuje nazwę konta użytkownika.
        /// </summary>
        [Display(Name = "Imię i Nazwisko")]
        public string Name { get; set; }

        /// <summary>
        /// Opisuje adres zamieszkania użytkownika konta.
        /// </summary>
        [Display(Name = "Adres")]
        public string Address { get; set; }

        /// <summary>
        /// Opisuje numer telefonu użytkownika.
        /// </summary>        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Opisuje adres mailowy konta użytkownika.
        /// </summary>        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Opisuje hasło dostępu do konta użytkownika.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string Password { get; set; }

        /// <summary>
        /// Opisuje potwierdzenie hasła dostępu do konta użytkownika.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        [Display(Name = "Powtórz nowe hasło")]
        public string ConfirmPassword { get; set; }
    }
}
