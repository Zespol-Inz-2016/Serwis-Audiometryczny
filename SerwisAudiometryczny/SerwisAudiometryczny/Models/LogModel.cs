using System;

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
        public int? UserId { get; set; }

        /// <summary>
        /// Zmienna przechowywująca gdzie zrobiono.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Zmienna przechowywująca co zrobiono.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Zmienna przechowywująca czas o której coś zrobiono.
        /// </summary>
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