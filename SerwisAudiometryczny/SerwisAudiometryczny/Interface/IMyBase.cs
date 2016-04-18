using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Interface
{
    interface IMyBase
    {
        int ID
        {
            get;
            set;
        }
        DateTime CreationDate
        {
            get;
            set;
        }
        string Type
        {
            get;
            set;
        }
    }
}
