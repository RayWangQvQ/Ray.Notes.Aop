using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shares
{
    public class PrintHelper
    {
        public static void Print(string str)
        {
            Console.WriteLine(str);
            Debug.WriteLine(str);
        }
    }
}
