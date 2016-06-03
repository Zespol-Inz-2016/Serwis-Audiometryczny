using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

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
        public enum Genders { Male, Female }
        /// <summary>
        /// Typ wyliczeniowy dla Uciążliwości uszkodzenia słuchu.
        /// </summary>
        public enum Nuisances { Unspecified = 0, None = 1, Slight = 2, Perceptible = 3, Moderate = 4, Significant = 5, Strong = 6, Extreme = 7 }
        /// <summary>
        /// Opisuje poziom uszkodzenia słuchu dla lewego ucha.
        /// </summary>
        public HearingLevel LeftEar { get; set; }
        /// <summary>
        /// Opisuje poziom uszkodzenia słuchu dla prawego ucha.
        /// </summary>
        public HearingLevel RightEar { get; set; }
        /// <summary>
        /// Opisuje diagnozę wystawioną pacjentowi.
        /// </summary>
        [Required(ErrorMessage = "Proszę podać diagnozę.")]
        [StringLength(150,ErrorMessage = "Diagnoza nie powinna być dłuższa nić 150 znaków.")]
        public string Diagnosis { get; set; }
        /// <summary>
        /// Opisuje płeć pacjenta/pacjentki.
        /// </summary>
        public Genders Gender { get; set; }
        /// <summary>
        /// Opisuje stopień uciążliwości uszkodzenia słuchu.
        /// </summary>
        public Nuisances Nuisance { get; set; }
        /// <summary>
        /// Opisuje wiek pacjenta.
        /// </summary>
        [Range(0, 150, ErrorMessage = "Wiek nie może być ujemny lub jest zbyt duży.")]
        [Required(ErrorMessage = "Podanie wieku jest wymagane.")]
        public int Age { get; set; }
        /// <summary>
        /// Opisuje utratę słuchu w procentach.
        /// </summary>
        [Required(ErrorMessage = "Proszę podać procent utraty słuchu.")]
        [Range(typeof(float), "0","100")]
        public float PercentageHearingLoss { get; set; }
        /// <summary>
        /// Opisuje, czy pacjent jest muzykiem.
        /// </summary>
        public bool IsMusician { get; set; }
        /// <summary>
        /// Opisuje główny instrument, którego używa pacjent.
        /// </summary>
        public InstrumentModel Instrument { get; set; }
        /// <summary>
        /// Opisuje, do którego pacjenta należy ten audiogram.
        /// </summary>
        public int PatientID { get; set; }
        /// <summary>
        /// Opisuje, który lekarz(edytor) dodał ten audiogram.
        /// </summary>
        public int EditorID { get; set; }
    }
}