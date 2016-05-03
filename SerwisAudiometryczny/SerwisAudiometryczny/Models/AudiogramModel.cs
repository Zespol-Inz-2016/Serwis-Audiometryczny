using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    public class AudiogramModel : BaseModel, IMyBase
    {
        /// <summary>
        /// Typ wyliczeniowy dla płci.
        /// </summary>
        public enum Sexes { Male, Female }
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
        public string Diagnosis { get; set; }
        /// <summary>
        /// Opisuje płeć pacjenta/pacjentki.
        /// </summary>
        public Sexes Sex { get; set; }
        /// <summary>
        /// Opisuje stopień uciążliwości uszkodzenia słuchu.
        /// </summary>
        public Nuisances Nuisance { get; set; }
        /// <summary>
        /// Opisuje wiek pacjenta.
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Opisuje utratę słuchu w procentach.
        /// </summary>
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