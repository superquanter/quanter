using Akka.Actor;
using Akka.Event;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;

namespace Quanter.Strategy
{
    /// <summary>
    /// 接收的消息
    /// 1、价格变动
    /// 2、启动策略
    /// 3、停止策略
    /// 
    /// 发出的请求
    /// 1、订阅证券价格              证券市场
    /// 2、取消订阅证券价格          证券市场 
    /// 3、下单操作                  交易接口
    /// </summary>
    public class StrategyActor : TypedActor 
    {
        private readonly ILoggingAdapter log = Logging.GetLogger(Context);

        private ActorSelection _market = null;

        IStrategy strategy = null;

        public StrategyActor(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        private void _start()
        {
            if (strategy.TradeMode == TradeMode.BACK_TEST)
            {
                // 获取历史行情
                _market = Context.ActorSelection("akka.tcp://myServer@localhost:8081/securities/market/history");
            }
            else
            {
                // 
                _market = Context.ActorSelection("akka.tcp://myServer@localhost:8081/securities/market");
            }
            _watchStock();
            strategy.Start();
        }

        private void _stop()
        {
            _unwatchStock();
            strategy.Stop();
        }

        private void _tickData(TickData data)
        {
            strategy.OnTickData();
        }

        /// <summary>
        /// Bar数据达到
        /// </summary>
        /// <param name="data"></param>
        private void _barData(TickData data)
        {
            strategy.OnBarData();
        }

        private void _quoteData()
        {
            strategy.OnQuoteData();
        }

        private void _run()
        {
            strategy.Run();
        }

        /// <summary>
        /// 创建一个策略
        /// </summary>
        private void _createStrategy()
        {
            strategy = null;
        }
        
        /// <summary>
        /// 下单操作
        /// </summary>
        private void _createOrder(String symbol, float price, int amount, OrderType type)
        {
            // IAccount account = strategy as IAccount;
        }

        /// <summary>
        /// 订阅股票价格
        /// </summary>
        private void _watchStock()
        {
            foreach (var item in strategy.SecuritiesList)
            {
                _market.Tell("订阅价格");
            }
        }

        /// <summary>
        /// 取消订阅股票价格
        /// </summary>
        private void _unwatchStock()
        {
            foreach (var item in strategy.SecuritiesList)
            {
                _market.Tell("取消订阅价格");
            }
        }

    }
}