using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.BusinessEntity
{
    public class EStockHolder
    {
        public EStrategy Strategy { get; set; }
        public virtual String Symbol { get; set; }
        public virtual String Name { get; set; }
        public virtual float MarketValue { get { return LastPrice * CurrentAmount; } }
        public virtual float CostPrice { get; set; }
        public virtual int CurrentAmount { get; set; }
        public virtual int EnableAmount { get; set; }
        public virtual int IncomeAmount { get; set; }  // 冻结
        public virtual float LastPrice { get; set; }

    }
}
