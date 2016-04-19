using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models.Dicts
{
    //[Table("DictDiagnosisModel")]
    public class DictDiagnosisModel : DictBaseModel
    {
        public DictDiagnosisModel() : base()
       {
            Type = "DictDiagnosis";
        }
    }
}