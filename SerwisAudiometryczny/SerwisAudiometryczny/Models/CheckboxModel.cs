using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    public class CheckboxModel
    {
        public bool Value { get; set; }
        public string Label { get; set; }
        public CheckboxModel(bool value,string label)
        {
            Value = value;
            Label = label;
        }
    }
}