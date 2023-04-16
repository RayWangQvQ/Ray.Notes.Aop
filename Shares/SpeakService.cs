using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shares
{
    public class SpeakService: ISpeakService
    {
        public void Say()
        {
            PrintHelper.Print("Hello World!");
        }
    }
}
