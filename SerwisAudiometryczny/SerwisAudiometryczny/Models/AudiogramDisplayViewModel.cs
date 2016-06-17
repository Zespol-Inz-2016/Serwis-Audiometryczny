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
        /// <summary>
        /// Przekazuje, czy wyświetlać dane pacjenta
        /// </summary>
        public bool ShowPatientData { get; set; }
        /// <summary>
        /// Przekazuje, czy wyświetlać dane edytora
        /// </summary>
        public bool ShowEditorData { get; set; }
    }
}