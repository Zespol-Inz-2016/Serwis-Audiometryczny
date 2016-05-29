using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model częstotliwości.
    /// </summary>
    [Serializable]
    public class FrequencyModel : BaseModel
    {
        /// <summary>
        /// Opisuje częstotliwość.
        /// </summary>
        public int Frequency { get; set; }
    }
}