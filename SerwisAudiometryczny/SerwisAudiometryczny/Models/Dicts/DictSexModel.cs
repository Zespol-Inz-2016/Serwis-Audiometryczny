using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models.Dicts
{
    //[Table("DictSexModel")]
    public class DictSexModel : DictBaseModel
    {
       public DictSexModel() : base()
       {
            Type = "DictSex";
       }
    }
}