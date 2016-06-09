using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisAudiometryczny.Models
{
    /// <summary>
    /// Model dla checkbox wykozystywany w generowaniu widoku
    /// </summary>
    public class CheckboxModel
    {
        /// <summary>
        /// Zmienna wartości
        /// </summary>
        public bool Value { get; set; }
        /// <summary>
        /// Zmienna podpisu
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Wartosc chexboxa</param>
        /// <param name="label">Podpis naszego chexbox'a</param>
        public CheckboxModel(bool value,string label)
        {
            Value = value;
            Label = label;
        }
    }
}