using System;
using System.ComponentModel.DataAnnotations;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model instrumentu.
    /// </summary>
    [Serializable]
    public class InstrumentModel : BaseModel
    {
        /// <summary>
        /// Nazwa instrumentu.
        /// </summary>
        [Display(Name = "Nazwa instrumentu")]
        public string Name { get; set; }
    }
}