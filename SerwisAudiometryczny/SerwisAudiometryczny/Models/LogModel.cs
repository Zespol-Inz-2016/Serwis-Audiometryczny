using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Models
{
    public class LogModel : MyBaseModel
    {
        public string IdUser { get; set; }  
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        /// <summary>
        /// IdUser - zmienna przechowywująca numer indentyfikacyjny użytkownika
        /// Controller - zmienna przechowywująca gdzie zrobiono
        /// Action - zmienna przechowywująca co zrobiono 
        /// Time - zmienna przechowywująca czas o której coś zrobiono
        /// </summary>
        public override string ToString()
        {
            return string.Format("Date: {0}, user ID: {1}, controller: {2}, action: {3}", Time, IdUser, Controller, Action);
            /// <returns>
            /// Metoda nadpisuje ToString() i zwraca model logu w jednym stringu
            /// </returns>
        }
    }
}