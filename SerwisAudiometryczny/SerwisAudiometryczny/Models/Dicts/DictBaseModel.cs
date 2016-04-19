using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models.Dicts
{
    public abstract class DictBaseModel : MyBaseModel, IDictBase
    {

        public DictBaseModel() : base()
        {
            Type = "DictBase";
        }

        public string Code
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}