using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models
{
    public class UserEditModelView
    {
        /// <summary>
        /// Opisuje imię użytkownika.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Opisuje adres zamieszkania użytkownika.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Opisuje adres mailowy użytkownika.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Opisuje hasło dostępu do konta użytkownika.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Opisuje typ użytkownika.
        /// </summary>
        public string Role { get; set; }
    }
}
