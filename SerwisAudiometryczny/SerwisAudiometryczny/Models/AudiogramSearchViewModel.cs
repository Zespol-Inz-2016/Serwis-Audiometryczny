namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Służy przekazaniu wszystkich potrzebnych informacji o audiogramach do widoku wyszukiwarki.
    /// </summary>
    public class AudiogramSearchViewModel
    {
        /// <summary>
        /// Typ wyliczeniowy dla płci.
        /// </summary>
        public enum Genders { Male, Female }
        /// <summary>
        /// Opisuje imię pacjenta.
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// Opisuje diagnozę wystawioną pacjentowi.
        /// </summary>
        public string Diagnosis { get; set; }
        /// <summary>
        /// Opisuje płeć pacjenta/pacjentki.
        /// </summary>
        public Genders? Gender { get; set; }
        /// <summary>
        /// Opisuje początkowy zakres wieku.
        /// </summary>
        public int? ageFrom { get; set; }
        /// <summary>
        /// Opisuje końcowy zakres wieku.
        /// </summary>
        public int? ageTo { get; set; }
        /// <summary>
        /// Opisuje początkowy zakres utraty słuchu w procentach.
        /// </summary>
        public float? PercentageHearingLossFrom { get; set; }
        /// <summary>
        /// Opisuje końcowy zakres utraty słuchu w procentach.
        /// </summary>
        public float? PercentageHearingLossTo { get; set; }
        /// <summary>
        /// Opisuje, czy pacjent jest muzykiem.
        /// </summary>
        public bool? isMusican { get; set; }
        /// <summary>
        /// Opisuje do którego pacjenta należy audiogram.
        /// </summary>
        public ApplicationUser Patient { get; set; }
        /// <summary>
        /// Opisuje, który lekarz(edytor) dodał audiogram.
        /// </summary>
        public ApplicationUser Editor { get; set; }
    }
}
