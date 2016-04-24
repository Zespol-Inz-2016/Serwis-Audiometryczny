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
        public AudiogramModel() : base()
        {
            Type = "Audiogram";
        }
        public int PatientID { get; set; }
        public PointModel[] Points { get; set; }
        public int DictDiagnosisModelID { get; set; }
        public int DictSexModelID { get; set; }
        public int DictNuisanceModelID { get; set; }
        public int DictInstrumentModelID { get; set; }
        public bool IsMusican { get; set; }
        public float PercentageHearingLoss { get; set; }
        public int PatientAge { get; set; }
    }
}