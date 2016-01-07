using Akka.Actor;
using Quanter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{
    public class ConnectedRequest
    {
        public String Username { get; set; }
        public String Password { get; set; }
    }

    public class ConnectedResponse
    {
        public String Message { get; set; }
    }

    public class MarketRequest
    {
        public enum RequestType
        {
            START,
            STOP,
            ADD_STRATEGY,
            REMOVE_STARTEGY,
        }

        public RequestType Type { get; set; }
    }

    public class StopMarketRequest
    {

    }

    public class AddStrategyRequest
    {
        private List<Securities> secs = new List<Securities>();
        public Securities[] Securitiess { get { return this.secs.ToArray(); } }

        public void AddSecurities(Securities sec)
        {
            secs.Add(sec);
        }
    }

    public class RemoveStrategyRequest
    {
        private List<Securities> secs = new List<Securities>();
        public Securities[] Securitiess { get { return this.secs.ToArray(); } }

        public void AddSecurities(Securities sec)
        {
            secs.Add(sec);
        }
    }

    /// <summary>
    /// 订阅股票数据请求
    /// </summary>
    public class WatchStockRequest
    {
        public String Symbol { get; set; }

        public IActorRef Watcher { get; set; }
    }

    /// <summary>
    /// 取消订阅股票数据的请求
    /// </summary>
    public class UnwatchStockRequest
    {
        public String Symbol { get; set; }

        public IActorRef Watcher { get; set; }
    }

    public class StockCurrentBidRequest
    {
        public String Symbol { get; set; }

        public IActorRef Watcher { get; set; }
    }

    public class NewBidArrivedRequest
    {
        public TickData Bid { get; set; }
    }

    public class StockDataResponse
    {

    }

    /// <summary>
    /// 股票历史数据请求
    /// </summary>
    public class StockHistoryDataRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Symbol { get; set; }
    }

    public class StockHistoryDataResponse
    {
        
    }


}
