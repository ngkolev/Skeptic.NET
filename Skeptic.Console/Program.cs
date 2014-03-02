using Skeptic.Core;
using Skeptic.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeptic.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var skeptic = new SkepticConsole(args);
            skeptic.Criticize();
        }
    }
}
