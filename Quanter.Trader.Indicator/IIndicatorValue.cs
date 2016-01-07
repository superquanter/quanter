using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Indicator
{
    public interface IIndicatorValue
    {
        DateTime Time { get; set; }
        float Value { get; set; }
    }
}
