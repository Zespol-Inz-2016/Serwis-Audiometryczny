using System.Collections.Generic;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Służy przekazaniu wszystkich potrzebnych modeli do widoku edycji.
    /// </summary>
    public class AudiogramCreateEditViewModel
    {
        /// <summary>
        /// Służy przekazaniu modelu audiogramu do i z widoku edycji.
        /// </summary>
        public AudiogramModel Audiogram { get; set; }
        /// <summary>
        /// Służy przekazaniu tablicy częstotliwości do i z widoku edycji.
        /// </summary>
        public int[] Frequencies { get; set; }
        /// <summary>
        /// Służy przekazaniu listy instrumentów do i z widoku edycji.
        /// </summary>
        public List<InstrumentModel> Instruments { get; set; }
        /// <summary>
        /// Służy przekazaniu nowego instrumentu z widoku edycji.
        /// </summary>
        public InstrumentModel NewInstrument { get; set; }
    }
}