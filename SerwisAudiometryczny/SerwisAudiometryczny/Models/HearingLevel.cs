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
        [NotMapped]
        public float[] Levels 
        { 
            get { 
                return Array.ConvertAll(InternalLevels.Split(';'), Float.Parse); 
            } 
            set { 
                InternalLevels = String.Join(";", value.Select(p => p.ToString()).ToArray()); 
            } 
        }
        /// <summary>
        /// Zawiera pooddzielane średnikami kolejne częstotliwości.
        /// </summary>
        public string InternalLevels { get; set; }
    }
}
