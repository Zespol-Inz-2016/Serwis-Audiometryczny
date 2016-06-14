using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Częstotliwość [MHz]")]
        public int Frequency { get; set; }
    }
}