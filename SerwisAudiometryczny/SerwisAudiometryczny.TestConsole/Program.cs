using SerwisAudiometryczny.Controllers.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LogControllerTests lct = new LogControllerTests();
            lct.LogSearchTest();
            Console.ReadKey();
        }
    }
}
