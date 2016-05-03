using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    public class InstrumentModel : BaseModel, IBaseModel
    {
        /// <summary>
        /// Nazwa instrumentu.
        /// </summary>
        public string Name { get; set; }
    }
}