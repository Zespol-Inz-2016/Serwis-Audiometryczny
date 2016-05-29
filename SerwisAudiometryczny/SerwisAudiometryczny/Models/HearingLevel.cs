using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model poziomów słyszalności.
    /// </summary>
    [Serializable]
    public class HearingLevel
    {
        /// <summary>
        /// Opisuje poziomy słyszalności w dB.
        /// </summary>
        [NotMapped]
        public float[] Levels 
        { 
            get { 
                return Array.ConvertAll(InternalLevels.Split(';'), float.Parse); 
            } 
            set { 
                InternalLevels = String.Join(";", value.Select(p => p.ToString()).ToArray()); 
            } 
        }
        [XmlIgnore]
        /// <summary>
        /// Zawiera pooddzielane średnikami kolejne częstotliwości.
        /// </summary>
        public string InternalLevels { get; set; }
    }
}
