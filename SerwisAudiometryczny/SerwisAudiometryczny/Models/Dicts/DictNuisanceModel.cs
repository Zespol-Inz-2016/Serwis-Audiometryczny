using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models.Dicts
{
    //[Table("DictNuisanceModel")]
    public class DictNuisanceModel : DictBaseModel
    {
        public DictNuisanceModel() : base()
       {
            Type = "DictInstrument";
        }
    }
}