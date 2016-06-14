using SerwisAudiometryczny.Helpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Przestrzeń nazw dla modeli.
/// </summary>
namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model audiogramu.
    /// </summary>
    [Serializable]
    public class AudiogramModel : BaseModel
    {
        /// <summary>
        /// Typ wyliczeniowy dla płci.
        /// </summary>
        public enum Genders {
            [Display(Name = "Mężczyzna")]
            Male,
            [Display(Name = "Kobieta")]
            Female
        }
        /// <summary>
        /// Typ wyliczeniowy dla Uciążliwości uszkodzenia słuchu.
        /// </summary>
        public enum Nuisances {
            [Display(Name = "Nieokreślona")]
            Unspecified = 0,
            [Display(Name = "Bez przeszkód")]
            None = 1,
            [Display(Name = "Lekka")]
            Slight = 2,
            [Display(Name = "Odczuwalna")]
            Perceptible = 3,
            [Display(Name = "Umiarkowana")]
            Moderate = 4,
            [Display(Name = "Znacząca")]
            Significant = 5,
            [Display(Name = "Wysoka")]
            Strong = 6,
            [Display(Name = "Ogromna")]
            Extreme = 7
        }
        /// <summary>
        /// Opisuje poziom uszkodzenia słuchu dla lewego ucha.
        /// </summary>
        [Display(Name = "Lewe ucho")]
        public HearingLevel LeftEar { get; set; }
        /// <summary>
        /// Opisuje poziom uszkodzenia słuchu dla prawego ucha.
        /// </summary>
        [Display(Name = "Prawe ucho")]
        public HearingLevel RightEar { get; set; }
        /// <summary>
        /// Opisuje diagnozę wystawioną pacjentowi.
        /// </summary>
        [Required(ErrorMessage = "Proszę podać diagnozę.")]
        [Display(Name = "Diagnoza")]
        [StringLength(150,ErrorMessage = "Diagnoza nie powinna być dłuższa nić 150 znaków.")]
        public string Diagnosis { get; set; }
        /// <summary>
        /// Opisuje płeć pacjenta/pacjentki.
        /// </summary>
        [Display(Name = "Płeć")]
        public Genders Gender { get; set; }
        /// <summary>
        /// Opisuje stopień uciążliwości uszkodzenia słuchu.
        /// </summary>
        [Display(Name = "Uciążliwość")]
        public Nuisances Nuisance { get; set; }
        /// <summary>
        /// Opisuje wiek pacjenta.
        /// </summary>
        [Range(0, 150, ErrorMessage = "Wiek nie może być ujemny lub jest zbyt duży.")]
        [Required(ErrorMessage = "Podanie wieku jest wymagane.")]
        [Display(Name = "Wiek")]
        public int Age { get; set; }
        /// <summary>
        /// Opisuje utratę słuchu w procentach.
        /// </summary>
        [Required(ErrorMessage = "Proszę podać procent utraty słuchu.")]
        [Range(typeof(float), "0","100")]
        [Display(Name = "Ubytek słuchu (%)")]
        public float PercentageHearingLoss { get; set; }
        /// <summary>
        /// Opisuje, czy pacjent jest muzykiem.
        /// </summary>
        [Display(Name = "Muzyk")]
        public bool IsMusician { get; set; }
        /// <summary>
        /// Opisuje główny instrument, którego używa pacjent.
        /// </summary>
        [Display(Name = "Instrument")]
        public InstrumentModel Instrument { get; set; }
        /// <summary>
        /// Opisuje, do którego pacjenta należy ten audiogram.
        /// </summary>
        [Display(Name = "ID pacjenta")]
        public int PatientID { get; set; }
        /// <summary>
        /// Opisuje, który lekarz(edytor) dodał ten audiogram.
        /// </summary>
        [Display(Name = "ID edytora")]
        public int EditorID { get; set; }
    }
}