using Akka.Actor;
using Akka.Event;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Market
{
    /// <summary>
    /// 处理接收的消息有
    /// 1、订阅价格
    /// 2、取消订阅价格
    /// 3、请求当前报价
    /// 4、设置当前价格
    /// </summary>
    public class SecuritiesQuotationActor : TypedActor, IHandle<SecuritiesQuotationRequest>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private Dictionary<String, ActorSelection> askTickActors = new Dictionary<String, ActorSelection>();
        private Dictionary<String, ActorSelection> askBarActors = new Dictionary<String, ActorSelection>();
        private Dictionary<String, ActorSelection> askQuotationActors = new Dictionary<String, ActorSelection>();

        private ActorSelection persistenceActor = null;
        private Securities securities;

        public SecuritiesQuotationActor(Securities securities)  
        {
            this.securities = securities;
        }

        protected override void PreStart()
        {
            _init();
            base.PreStart();
        }

        public void Handle(SecuritiesQuotationRequest message)
        {
            try {
                switch (message.Type)
                {
                    case SecuritiesQuotationRequest.RequestType.NEW_QUOTEDATA:
                        _receiveQuoteData((QuoteData)message.Body);
                        break;
                    case SecuritiesQuotationRequest.RequestType.NEW_BARDATA:
                        break;
                    case SecuritiesQuotationRequest.RequestType.NEW_TICKDATA:
                        break;
                    case SecuritiesQuotationRequest.RequestType.WATCH_QUOTEDATA:
                        _watchQuoteData(message.Body.ToString());
                        break;
                    case SecuritiesQuotationRequest.RequestType.WATCH_BARDATA:
                        _watchBarData(message.Body.ToString());
                        break;
                    case SecuritiesQuotationRequest.RequestType.WATCH_TICKDATA:
                        _watchTickData(message.Body.ToString());
                        break;
                    case SecuritiesQuotationRequest.RequestType.UNWATCH:
                        _unwatch(message.Body.ToString());
                        break;
                    default:
                        _log.Warning("不支持该操作 {0}", message.Type);
                        break;
                }
            }
            catch (Exception e)
            {
                _log.Error("SecuritiesQuotationActor.Handle<SecuritiesQuotationRequest>发生异常：{0}", e.StackTrace);
            }

        }

        private void _init()
        {
            _log.Debug("初始化{0}SecuritiesActor", securities.Symbol);
            persistenceActor = Context.ActorSelection(String.Format("/user/{0}", ConstantsHelper.AKKA_PATH_PERSISTENCE));
        }

        private void _receiveQuoteData(QuoteData data)
        {
            _log.Debug("{0}新报价数据到达，通知订阅的策略", data.Symbol);
            // TODO: 保存到数据库
            //PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = data };
            //persistenceActor.Tell(req);

            foreach(var actor in askQuotationActors.Values)
            {
                actor.Tell(new StrategyResponse() { Type = StrategyResponse.ResponseType.QUOTE_ARRIVED, Body = data });
            }
        }

        private void _watchQuoteData(String strategyId)
        {
            if (!this.askQuotationActors.ContainsKey(strategyId))
            {
                String path = String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_STRATEGY_MANAGER, strategyId);
                var actor = Context.ActorSelection(path);
                this.askQuotationActors.Add(strategyId, actor);
                _log.Debug("策略{0}订阅了{1}报价数据", strategyId, securities.Symbol);
            }
        }

        private void _watchBarData(String strategyId)
        {
            String path = String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_STRATEGY_MANAGER, strategyId);
            var actor = Context.ActorSelection(path);
            this.askBarActors.Add(strategyId, actor);
            _log.Debug("策略{0}订阅了{1}Bar数据", strategyId, securities.Symbol);
        }

        private void _watchTickData(String strategyId)
        {
            String path = String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_STRATEGY_MANAGER, strategyId);
            var actor = Context.ActorSelection(path);
            this.askTickActors.Add(strategyId, actor);
            _log.Debug("策略{0}订阅了{1}Tick数据", strategyId, securities.Symbol);
        }

        private void _unwatch(String strategyId)
        {
            if (this.askQuotationActors.Keys.Contains(strategyId)) this.askQuotationActors.Remove(strategyId);
            if (this.askBarActors.Keys.Contains(strategyId)) this.askBarActors.Remove(strategyId);
            if (this.askTickActors.Keys.Contains(strategyId)) this.askTickActors.Remove(strategyId);
        }
    }
}
