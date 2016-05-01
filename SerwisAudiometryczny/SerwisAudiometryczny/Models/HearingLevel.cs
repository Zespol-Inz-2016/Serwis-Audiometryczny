using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    public class HearingLevel
    {
        //
        //Udziela dostepu do zmiennej Levels[] w HearingLevel.
        public float[] Levels { get; set; }
        //
        //Udziela dostepu do zmiennej InternalLevels w HearingLevel.
        public string InternalLevels { get; set; }
    }
}