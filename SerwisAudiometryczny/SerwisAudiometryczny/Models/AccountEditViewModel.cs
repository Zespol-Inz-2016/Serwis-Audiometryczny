using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    class AccountEditViewModel
    { 
        /// <summary>
        /// Opisuje nazwę konta użytkownika.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Opisuje adres zamieszkania użytkownika konta.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Opisuje adres mailowy konta użytkownika.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Opisuje hasło dostępu do konta użytkownika.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Opisuje potwierdzenie hasła dostępu do konta użytkownika.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
