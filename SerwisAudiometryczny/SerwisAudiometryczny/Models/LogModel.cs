using System;
using System.ComponentModel.DataAnnotations;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model przechowywujący dane o zdarzeniu do zalogowania
    /// </summary>
    [Serializable]
    public class LogModel : BaseModel
    {
        /// <summary>
        /// Zmienna przechowywująca numer indentyfikacyjny użytkownika.
        /// </summary>
        [Display(Name = "Id użytkownika")]
        public int? UserId { get; set; }

        /// <summary>
        /// Zmienna przechowywująca gdzie zrobiono.
        /// </summary>
        [Display(Name = "Kontroler")]
        public string Controller { get; set; }

        /// <summary>
        /// Zmienna przechowywująca co zrobiono.
        /// </summary>
        [Display(Name = "Akcja")]
        public string Action { get; set; }

        /// <summary>
        /// Zmienna przechowywująca czas o której coś zrobiono.
        /// </summary>
        [Display(Name = "Czas zdarzenia")]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Metoda nadpisuje ToString()
        /// <returns>Model logu w jednym stringu</returns>
        /// </summary>
        public override string ToString()
        {
            return string.Format("Date: {0}, user ID: {1}, controller: {2}, action: {3}", Time, UserId, Controller, Action);
        }
    }
}