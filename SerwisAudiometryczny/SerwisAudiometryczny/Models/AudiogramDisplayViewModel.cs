using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{        
    /// <summary>
    /// Służy przekazaniu wszystkich potrzebnych modeli do widoku wyświetlania.
    /// </summary>
    public class AudiogramDisplayViewModel
    {
        /// <summary>
        /// Służy przekazaniu modelu audiogramu do widoku wyświetlania.
        /// </summary>
        public AudiogramModel Audiogram { get; set; }
        /// <summary>
        /// Służy przekazaniu modelu Pacjenta do widoku wyświetlania.
        /// </summary>
        public ApplicationUser Patient { get; set; }
        /// <summary>
        /// Służy przekazaniu modelu Edytora do widoku wyświetlania.
        /// </summary>
        public ApplicationUser Editor { get; set; }
        /// <summary>
        /// Służy przekazaniu tablicy częstotliwości do widoku wyświetlania.
        /// </summary>
        public int[] Frequencies { get; set; }
    }
}