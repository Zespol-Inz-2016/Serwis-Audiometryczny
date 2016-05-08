using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    public class HearingLevel
    {
        /// <summary>
        /// Opisuje poziomy słyszalności w dB.
        /// </summary>
        public float[] Levels { get; set; }
        /// <summary>
        /// Zawiera pooddzielane średnikami kolejne częstotliwości.
        /// </summary>
        public string InternalLevels { get; set; }
    }
}