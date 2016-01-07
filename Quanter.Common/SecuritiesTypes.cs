using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public enum SecuritiesTypes
    {
        Stock,
        Future,
        Option,
        Commodity,
        Cfd,
        Swap
    }

    public enum MarketType
    {
        XSHG,   // 上交所
        XSHE,   // 深交所
    }
}
