using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Interface
{
    interface IDictBase
    {
        string Name
        {
            get;
            set;
        }
        string Code
        {
            get;
            set;
        }
        string Description
        {
            get;
            set;
        }
    }
}
