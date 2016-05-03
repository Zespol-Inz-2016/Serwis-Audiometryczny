using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model wyszukujący dane o zdarzeniu do zalogowania
    /// </summary>
    public class LogSearchViewModel
    {
        /// <summary>
        /// IdUser - zmienna przechowywująca numer indetyfikacyjny użytkownika, którego chcemy wyszukać w logach
        /// </summary>
        public int? IdUser { get; set; }

        /// <summary>
        /// Controller - zmienna przechowywująca miejsce, które chcemy wyszukać w logach
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action - zmienna przechowywująca akcję, którą chcemy wyszukać w logach
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// FromTime - zmienna przechowywująca czas od kiedy chcemy wyszukać w logach
        /// </summary>
        public DateTime FromTime { get; set; }

        /// <summary>
        /// ToTime - zmienna przechowywująca czas do kiedy chcemy wyszukać w logach
        /// </summary>
        public DateTime ToTime { get; set; }

    }

}