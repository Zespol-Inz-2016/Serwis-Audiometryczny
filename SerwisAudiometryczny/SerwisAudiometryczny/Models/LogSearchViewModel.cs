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
        /// Zmienna przechowywująca numer indetyfikacyjny użytkownika, którego chcemy wyszukać w logach
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Zmienna przechowywująca miejsce, które chcemy wyszukać w logach 
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Zmienna przechowywująca akcję, którą chcemy wyszukać w logach 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Zmienna przechowywująca czas od kiedy chcemy wyszukać w logach 
        /// </summary>
        public DateTime FromTime { get; set; }

        /// <summary>
        /// Zmienna przechowywująca czas do kiedy chcemy wyszukać w logach 
        /// </summary>
        public DateTime ToTime { get; set; }

    }

}