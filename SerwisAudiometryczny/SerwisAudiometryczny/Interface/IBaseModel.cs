using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Interface
{
    /// <summary>
    /// Model bazowy
    /// </summary>
    interface IBaseModel
    {
        /// <summary>
        /// ID, klucz główny
        /// </summary>
        int ID
        {
            get;
            set;
        }
    }
}
