using System;

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
        public string Name { get; set; }
    }
}