using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Indicator
{
    interface IComplexIndicator : IIndicator
    {
        IEnumerable<IIndicator> InnerIndicators { get; }

    }
}
