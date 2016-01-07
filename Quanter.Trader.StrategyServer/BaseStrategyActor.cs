using Akka.Actor;
using Akka.Event;
using Quanter.BusinessEntity;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy
{
    public abstract class BaseStrategyActor : TypedActor, IHandle<StrategyRequest>, IHandle<StrategyResponse>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private ActorSelection persistenceActor = null;
        private ActorSelection tradeActor = null;
        protected List<Securities> secs = new List<Securities>();
        private Dictionary<String, Securities> secDict = new Dictionary<string, Securities>();
        private Dictionary<String, ActorSelection> symbolPriceActors = new Dictionary<string, ActorSelection>();
        private Dictionary<String, float> symbolPrices = new Dictionary<string, float>();

        protected void AddSecurities(Securities sec)
        {
            secDict.Add(sec.Symbol, sec);

            String path = String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_MARKET_MANAGER, sec.Symbol);
            var secActor = Context.ActorSelection(path);
            SecuritiesQuotationRequest req = new SecuritiesQuotationRequest()
            {
                Type = SecuritiesQuotationRequest.RequestType.WATCH_QUOTEDATA,
                Body = Desc.Id
            };
            secActor.Tell(req);
            symbolPriceActors.Add(sec.Symbol, secActor);
        }

        protected void RemoveSecurities(Securities sec)
        {
            if (secDict.ContainsKey(sec.Symbol)) {
                secDict.Remove(sec.Symbol);
                symbolPriceActors[sec.Symbol].Tell(new SecuritiesQuotationRequest() { Type = SecuritiesQuotationRequest.RequestType.UNWATCH, Body = Desc.Id });
                symbolPriceActors.Remove(sec.Symbol);
            }
        }

        public BaseStrategyActor(EStrategy strategy)
        {
            this.Desc = strategy;
        }
        public EStrategy Desc { get; private set; }

        public void Handle(StrategyRequest message)
        {
            switch(message.Type)
            {
                case StrategyRequestType.START:
                    start();
                    break;
                case StrategyRequestType.STOP:
                    stop();
                    break;
                default:
                    otherAction(message);
                    break;
            }
        }

        public void Handle(StrategyResponse message)
        {
            switch(message.Type)
            {
                case StrategyResponse.ResponseType.TICK_ARRIVED:
                    onTickData((TickData)message.Body);
                    break;
                case StrategyResponse.ResponseType.BAR_ARRIVED:
                    onBarData((BarData)message.Body);
                    break;
                case StrategyResponse.ResponseType.QUOTE_ARRIVED:
                    onQuoteData((QuoteData)message.Body);
                    break;
                case StrategyResponse.ResponseType.RUN_ARRIVED:
                    run(message.Body);
                    break;
            }
        }

        protected override void PreStart()
        {
            init();
            start();
            base.PreStart();
        }

        protected override void PostStop()
        {
            base.PostStop();
            stop();
        }

        protected virtual void init()
        {
            //String path = String.Format("/user/{0}", ConstantsHelper.AKKA_PATH_PERSISTENCE);
            //persistenceActor = Context.ActorSelection(path);

            String path = String.Format("/user/{0}", ConstantsHelper.AKKA_PATH_TRADER);
            tradeActor = Context.ActorSelection(path);

            initSettings();

        }

        protected virtual void initRisk() {
            // 初始化使用哪些风控
        }

        protected virtual void initSettings() {
            // 使用哪个交易接口
            // 使用的哪种类型的行情数据
            // 初始化配置参数
        }

        protected virtual void initSecurities() {
            // 关注哪些股票
        }

        protected virtual void start()
        {

        }

        protected virtual void stop()
        {

        }

        protected virtual void onTickData(TickData data)
        {

        }

        protected virtual void onBarData(BarData data)
        {

        }
        protected virtual void onQuoteData(QuoteData data)
        {

        }

        protected virtual void run(object data)
        {

        }

        protected virtual void otherAction(StrategyRequest message)
        {
        }

        #region 子账户的处理

        protected int getCurrentAmount(String symbol)
        {
            //foreach (StockHolderInfo shi in this.account.Holders)
            //{
            //    if (shi.StockCode == StockUtil.GetShortCode(code))
            //    {
            //        return shi.CurrentAmount;
            //    }
            //}
            return 0;
        }

        protected int getEnableAmount(String symbol)
        {
            //foreach (StockHolderInfo shi in this.account.Holders)
            //{
            //    if (shi.StockCode == StockUtil.GetShortCode(code))
            //    {
            //        return shi.EnableAmount;
            //    }
            //}
            return 0;
        }

        protected void buySecurities(Securities securities, float price, int amount)
        {
            // 1、下单
            Order order = _createOrder(securities, price, amount, OrderType.BUY);
            _notifyTrader(order);       
        }

        protected void cancelSecurities(int entrustNo)
        {
            _log.Debug("保存到策略账户 委撤单 策略ID:{0}, 合同号:{1}", Desc.Id, entrustNo);
        }

        protected void sellSecurities(Securities securities, float price, int amount)
        {
            // 下单
            Order order = _createOrder(securities, price, amount, OrderType.SELL);
            _notifyTrader(order);
        }

        private Order _createOrder(Securities sec, float price, int amount, OrderType type)
        {
            Order order = new Order
            {
                Amount = amount,
                Price = price,
                Type = type,
                Symbol = sec.Symbol,
                StrategyId = Desc.Id
            };

            return order;
        }

        private void _notifyTrader(Order order)
        {
            _log.Debug("通知交易端下单 策略ID:{0}, 代码:{1}, 证券类别: {2}, 价格:{3}， 数量:{4}", Desc.Id, order.Symbol);

            TradeRequest req = new TradeRequest();

            req.OrderType = order.Type;
            req.TradeInterface = TradeInterface.THS;
            req.TradeType = TradeType.ALGO;

            req.SecuritiesOrder = order;

            tradeActor.Tell(req); // TODO: 改为ASK?
        }

        private void _updateAccountInfo ()
        {
            // Ask 账户持股信息，资金信息

            // 
        }

        /// <summary>
        /// 由Persistence Actor来做,迁移到Trader 接口去保存订单
        /// </summary>
        /// <param name="order"></param>
        //private void _saveEntrustOrder(Order order)
        //{
        //    PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = order };

        //    persistenceActor.Tell(req);
        //}

        #endregion

        protected string _getSymbol(String stockCode)
        {
            if (stockCode.Length == 6)
            {
                switch (stockCode.Substring(0, 2))
                {
                    case "51":
                    case "50":
                    case "60":
                        stockCode = stockCode + ".XSHG";
                        return stockCode;

                    case "00":
                    case "15":
                    case "16":
                    case "30":
                        stockCode = stockCode + ".XSHE";
                        return stockCode;
                }
            }
            else if (stockCode.Length == 8)
            {
                switch (stockCode.Substring(0, 2))
                {
                    case "sz":
                        stockCode = stockCode.Substring(2, 6) + ".XSHE";
                        break;
                    case "sh":
                        stockCode = stockCode.Substring(2, 6) + ".XSHG";
                        break;
                }
            }

            return stockCode;
        }

        protected string _getShortCode(String code)
        {
            switch (code.Length)
            {
                case 6:
                    return code;
                case 8:
                    return code.Substring(2);
                case 13:
                    return code.Substring(0, 6);
                default:
                    return code;
            }
        }
    }
}
