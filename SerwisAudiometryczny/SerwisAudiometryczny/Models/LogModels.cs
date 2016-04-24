using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Models
{
    public class LogModels
    {
        public string idUser { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public DateTime time { get; set; }

        public override string ToString()
        {
            return string.Format("Date: {0}, user ID: {1}, controller: {2}, action: {3}", time, idUser, controller, action);
        }
    }
}