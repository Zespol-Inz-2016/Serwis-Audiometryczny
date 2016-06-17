using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerwisAudiometryczny.Models
{
    public class LoginViewModel
    {

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }

        /// <summary>
        /// Zmienna ID
        /// </summary>
        [Required(ErrorMessage = "ID jest wymagane")]
        public string ID { get; set; }
    }
}
