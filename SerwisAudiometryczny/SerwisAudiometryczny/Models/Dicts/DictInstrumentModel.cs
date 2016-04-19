using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models.Dicts
{
    //[Table("DictInstrumentModel")]
    public class DictInstrumentModel : DictBaseModel
    {
        public DictInstrumentModel() : base()
       {
            Type = "DictInstrument";
        }
    }
}