using Akka.Actor;
using Akka.Event;
using Quanter.BusinessEntity;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;

namespace Quanter.Strategy.Demo
{
    public class DemoStrategyActor : BaseStrategyActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public DemoStrategyActor(EStrategy strategy) : base(strategy)
        {
            _log.Debug("创建编码为 {0} StrategyActor实例", strategy.Id);
        }

        protected override void onInit()
        {
            _log.Debug("初始化编号为{0}Strategy", Desc.Id);
            AddSecurities(new Securities(SecuritiesTypes.Stock, MarketType.XSHE, "000002"));
            AddSecurities(new Securities(SecuritiesTypes.Stock, MarketType.XSHG, "603998"));
        }


        protected override void onQuoteData(QuoteData data)
        {
            _log.Debug("Demo策略处理{0}报价数据", data.Symbol);
            this.buySecurities(new Securities( SecuritiesTypes.Stock, data.Symbol), data.SellPrice1, 100);
        }
    }
}
