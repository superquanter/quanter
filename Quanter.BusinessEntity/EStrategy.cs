using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.BusinessEntity
{
    public class EStrategy
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Desc { get; set; }
        public virtual String Type { get; set; }

        // public virtual bool Enabled { get; set; }
        // public virtual float CurrentBalance { get; set; }   // 资金余额
        // public virtual float FetchBalance { get; set; }     // 可取金额
        public virtual float EnableBalance { get; set; }    // 可用金额
        public virtual float FrozenBalance { get; set; }    // 冻结金额
        public virtual float MarketValue { get; set; }      // 股票市值
        public virtual float AssetBalance { get; set; }     // 总资产

        public virtual DateTime Date { get; set; }

        private List<EStockHolder> holders = new List<EStockHolder>();
        public virtual List<EStockHolder> Holders
        {
            get { return this.holders; }
            set { }
        }

    }
}
