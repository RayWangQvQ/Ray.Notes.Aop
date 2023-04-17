using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shares
{
    public class SingService : ISingService
    {
        public async Task Sing()
        {
            await Task.Delay(100);
            PrintHelper.Print("LaLaLa");

            await Task.Delay(100);
            PrintHelper.Print("LvLvLv");
        }
    }
}
