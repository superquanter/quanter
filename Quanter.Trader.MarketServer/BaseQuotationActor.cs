using Akka.Actor;
using Akka.Event;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quanter.Market
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseQuotationActor : TypedActor, IHandle<QuotationRequest>
    {
        protected readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        protected IDictionary<String, ActorSelection> symbolActors = new Dictionary<String, ActorSelection>();
        protected IList<String> aliases = new List<String>();

        public void Handle(QuotationRequest message)
        {
            try {
                switch (message.Type)
                {
                    case QuotationRequest.RequestType.ASKED_SECURITIES:
                        _handleAskedSecurities(message.Body as IList<Securities>);
                        break;
                    case QuotationRequest.RequestType.RUN:
                        run();
                        break;
                }
            }catch (Exception e)
            {
                _log.Error("BaseQuotationActor.Handle<QuotationRequest>发生异常：{0}", e.StackTrace);
            }
        }

        protected virtual void _handleAskedSecurities(IList<Securities> seces)
        {
            // 初始化证券Actor列表
            foreach (var sec in seces)
            {
                if (!aliases.Contains(sec.Alias))
                {
                    _log.Debug("准备接收证券代码 {0}的行情数据", sec.Symbol);
                    var actor = Context.ActorSelection(String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_MARKET_MANAGER, sec.Symbol));
                    symbolActors.Add(sec.Symbol, actor);

                    aliases.Add(sec.Alias);      // 用于获取数据
                }
            }
        }


        protected virtual void run()
        {
        }

        protected void newQuoteDataArrived(QuoteData data)
        {
            _log.Debug("通知股票{0}Actor，有新Quote数据到达", data.Alias);
            symbolActors[data.Symbol].Tell(new SecuritiesQuotationRequest() { Type = SecuritiesQuotationRequest.RequestType.NEW_QUOTEDATA, Body = data });
        }

    }
}
