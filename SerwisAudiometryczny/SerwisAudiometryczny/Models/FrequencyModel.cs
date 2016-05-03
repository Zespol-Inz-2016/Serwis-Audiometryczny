using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    public class FrequencyModel : BaseModel, IMyBase
    {
        /// <summary>
        /// Opisuje częstotliwość.
        /// </summary>
        public int Frequency { get; set; }
    }
}