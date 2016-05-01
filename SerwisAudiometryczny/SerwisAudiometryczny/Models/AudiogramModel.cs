using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    public class AudiogramModel : MyBaseModel, IMyBase
    {
        public AudiogramModel():base()
        {
            Type = "AudiogramModel";
        }
        //
        //Typ wyliczeniowy dla płci.
        public enum Sexes { Male, Female }
        //
        //Typ wyliczeniowy dla Uciążliwości uszkodzenia słuchu.
        public enum Nuisances { Unspecified = 0, None = 1, Slight = 2, Perceptible = 3, Moderate = 4, Significant = 5, Strong = 6, Extreme = 7 }
        //
        //Udziela dostępu do zmiennej LeftEar typu HearingLevel dla lewego ucha w AudiogramModel.
        public HearingLevel LeftEar { get; set; }
        //
        //Udziela dostępu do zmiennej RightEar typu HearingLevel dla prawego ucha w AudiogramModel.
        public HearingLevel RightEar { get; set; }
        //
        //Udziela dostępu do zmiennej Diagnosis w AudiogramModel. Opisuje diagnozę wystawioną pacjentowi.
        public string Diagnosis { get; set; }
        //
        //Udziela dostępu do zmiennej Gender w AudiogramModel. Opisuje płeć pacjenta/pacjentki.
        public Sexes Gender { get; set; }
        //
        //Udziela dostępu do zmiennej Nuisance w AudiogramModel. Opisuje stopień uciążliwości uszkodzenia słuchu.
        public Nuisances Nuisance { get; set; }
        //
        //Udziela dostępu do zmiennej Age w AudiogramModel. Opisuje wiek pacjenta.
        public int Age { get; set; }
        //
        //Udziela dostępu do zmiennej PercentageHearingLoss w AudiogramModel. Opisuje utratę słuchu w procentach.
        public float PercentageHearingLoss { get; set; }
        //
        //Udziela dostępu do zmiennej IsMusician w AudiogramModel. Opisuje, czy pacjent jest muzykiem.
        public bool IsMusician { get; set; }
        //
        //Udziela dostępu do zmiennej Instrument w AudiogramModel. Opisuje główny instrument, którego używa pacjent.
        public InstrumentModel Instrument { get; set; }
        //
        //Udziela dostępu do zmiennej Patient w AudiogramModel. Opisuje, do którego pacjenta należy ten audiogram.
        public ApplicationUser Patient { get; set; }
        //
        //Udziela dostępu do zmiennej EditorBackupController w AudiogramModel. Opisuje, który lekarz(edytor) dodał ten audiogram.
        public ApplicationUser Editor { get; set; }
    }
}