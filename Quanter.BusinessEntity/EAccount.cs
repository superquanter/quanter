using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.BusinessEntity
{
    public class EAccount
    {
        public virtual int Id { get; set; }
        public virtual EStrategy Strategy { get; set; }     // 策略
        public virtual float CurrentBalance { get; set; }   // 资金余额
        public virtual float EnableBalance { get; set; }    // 可用金额
        public virtual float FetchBalance { get; set; }     // 可取金额
        public virtual float FrozenBalance { get; set; }    // 冻结金额
        public virtual float MarketValue { get; set; }      // 股票市值
        public virtual float AssetBalance { get; set; }     // 总资产
        public virtual DateTime Date { get; set; }          // 最新资产的时间

        //private List<StockHolderInfo> holders = new List<StockHolderInfo>();
        //public virtual List<StockHolderInfo> Holders
        //{
        //    get { return this.holders; }
        //    set { }
        //}
    }
}
