using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerwisAudiometryczny.Models;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model przechowywujący dane o zdarzeniu do zalogowania
    /// </summary>
    public class LogModel : MyBaseModel
    {
        public LogModel() : base()
        {
            Type = "Log";
        }
        /// <summary>
        /// IdUser - zmienna przechowywująca numer indentyfikacyjny użytkownika
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Controller - zmienna przechowywująca gdzie zrobiono
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action - zmienna przechowywująca co zrobiono 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Time - zmienna przechowywująca czas o której coś zrobiono
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Metoda nadpisuje ToString()
        /// <returns>Model logu w jednym stringu</returns>
        /// </summary>
        public override string ToString()
        {
            return string.Format("Date: {0}, user ID: {1}, controller: {2}, action: {3}", Time, IdUser, Controller, Action);
        }
    }
}