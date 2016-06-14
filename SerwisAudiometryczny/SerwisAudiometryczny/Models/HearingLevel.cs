using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        /// <summary>
        /// Zawiera pooddzielane średnikami kolejne częstotliwości.
        /// </summary>
        [XmlIgnore] 
        [Display(Name = "Częstotliwości")]
        public string InternalLevels { get; set; }
    }
}
