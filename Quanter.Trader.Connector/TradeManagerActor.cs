using Akka.Actor;
using Akka.Event;
using Quanter.BusinessEntity;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public class TradeManagerActor : TypedActor, IHandle<TradeManagerRequest>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private ActorSelection persistenceActor;
        private IDictionary<int, IActorRef> traders = new Dictionary<int, IActorRef>();

        public void Handle(TradeManagerRequest message)
        {
            try
            {
                switch(message.Type)
                {
                    case TradeManagerRequest.RequestType.INIT:
                        _init();
                        break;
                }
            } catch (Exception e)
            {
                _log.Error("TradeManagerActor发生异常 {0}", e.StackTrace);
            }
        }

        private void _init()
        {
            _log.Debug("从数据库中读取交易接口的信息");
            persistenceActor = Context.ActorSelection(String.Format("/user/{0}", ConstantsHelper.AKKA_PATH_PERSISTENCE));
            // 读取交易接口
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.LIST, Body = "from ETrader" };
            var result = persistenceActor.Ask<IList<Object>>(req, TimeSpan.FromSeconds(3));
            result.Wait();
            var traders = result.Result;
            foreach(var o in traders)
            {
                var trader = (ETrader)o;
                _createTradeActor(trader);
                _log.Debug("加载交易接口 {0}", trader.Name);
            }
        }

        private void _createTradeActor(ETrader trader)
        {
            Type t = Type.GetType(trader.Type);
            var tr = Context.ActorOf(Props.Create(t), trader.Id.ToString());

            tr.Tell(new TradeRequest() { Type = TradeRequest.RequestType.INIT });

            traders.Add(trader.Id, tr);
        }

    }
}
