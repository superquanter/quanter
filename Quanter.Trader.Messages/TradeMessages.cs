using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{
    public enum OrderType {
        BUY,
        SELL,
        CANCEL,
    }

    public enum TradeType
    {
        ALGO,       //算法下单
        LIMIT,      // 涨跌停板下单
    }

    public enum TradeInterface
    {
        FIX,
        CTP,
        CTP2,
        LTS,
        THS,
        TDX,
    }

    public class Order
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 委托号 > 0,表示成功
        /// <=0表示有错误，错误标识在细化
        /// -1: 表示资金不足
        /// -2: 表示股票数量不足
        /// -3: 输入的证券代码不对
        /// </summary>
        public int EntrustNo { get; set; }

        public int StrategyId { get; set; }
        public String Symbol { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public OrderType Type { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime Date { get; set; }

    }

    public class TradeRequest
    {
        public enum RequestType
        {
            INIT,
            BUY,
            SELL,
        }

        public RequestType Type { get; set; }

        public OrderType OrderType { get; set; }

        public TradeInterface TradeInterface { get; set; }

        public TradeType TradeType { get; set; }

        public Order SecuritiesOrder { get; set; }
    }

    public class TradeResponse
    {
        public int EntrustNo { get; set; }
        public String Message { get; set; }
    }

    public class TradeManagerRequest
    {
        public enum RequestType
        {
            INIT,
        }

        public RequestType Type { get; set; }
        public Object Body { get; set; }
    }

}
