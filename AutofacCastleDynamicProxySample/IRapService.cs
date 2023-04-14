using Autofac.Extras.DynamicProxy;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacCastleDynamicProxySample
{
    [Intercept(typeof(MyInterceptor))]
    public interface IRapService
    {
        void Rap();
    }
}
