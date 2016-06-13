using System.ComponentModel.DataAnnotations;

namespace SerwisAudiometryczny.Models
{
    public class UserEditModelView
    {
        /// <summary>
        /// Id dla użytkownika
        /// </summary>
        [Display(Name = "Numer identyfikacyjny")]
        public int Id { get; set; }

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

    }
}
