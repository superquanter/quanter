using Akka.Actor;
using Akka.Event;
using Quanter.BusinessEntity;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy
{
    /// <summary>
    /// 处理的接收消息有
    /// 1、启动指定的策略 StartStrategiesRequest
    /// 2、注册策略
    /// 3、配置指定的策略 SetupStrategyRequest
    /// 4、停止所有的策略(depreated)
    /// 5、停止指定的策略 StopStrategiesRequest
    /// 6、请求所有的策略列表 StrategiesRequest
    /// 
    /// 发出的请求有
    /// 1、证券市场订阅关注的证券
    /// 2、证券市场取消关注的证券
    /// 3、
    /// </summary>
    public class StrategyManagerActor : TypedActor, IHandle<StrategyRequest>, IHandle<StrategiesRequest>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private Dictionary<int, ICanTell> strategyActors = new Dictionary<int, ICanTell>();

        public void Handle(StrategyRequest message)
        {
            switch(message.Type)
            {
                case StrategyRequestType.CREATE:
                    _createStrategyActor((EStrategy)message.Body);
                    break;
                case StrategyRequestType.START:
                case StrategyRequestType.STOP:
                    _log.Error("错误的Request Type {0}", message.Type);
                    break;
                default:
                    _log.Warning("不支持的Request Type {0}", message.Type);
                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public void Handle(StrategiesRequest message)
        {
            StrategiesResponse sr = new StrategiesResponse();

            sr.Add(new EStrategy
            {
                Type="",
                Name = "雪球跟踪策略",
                Desc = "跟踪雪球， 快速下单"
            });

            sr.Add(new EStrategy
            {
                Type = "",
                Name = "分级B强势轮动策略",
                Desc = "发现强势分级基金，快速下单"
            });

            Sender.Tell(sr);

            _log.Info("获取到所有的策略列表");
        }

        private void _createStrategyActor(EStrategy sd)
        {
            if (!strategyActors.ContainsKey(sd.Id))
            {
                // String[] ary = sd.Type.Split(new string[] { "," }, StringSplitOptions.None);
                Type t = Type.GetType(sd.Type);
                var strategyActor = Context.ActorOf(Props.Create(t, sd), sd.Id.ToString());
                strategyActors.Add(sd.Id, strategyActor);
            }else
            {
                _log.Warning("重复注册策略，策略号 {0}", sd.Id);
            }
        }

        //private void _registerStarategy(IStrategy strategy)
        //{
        //    if(strategyActors.Keys.Contains<int>(strategy.Id))
        //    {
        //        Sender.Tell(new ErrorResponse() { });        // Warning： the strategy is existed
        //        return;
        //    }
        //    IActorRef strategyActor = null;

        //    strategyActors.Add(strategy.Id, strategyActor);
        //}

        //private void _unregisterStrategy(IStrategy strategy)
        //{
        //    if (!strategyActors.Keys.Contains<int>(strategy.Id))
        //    {
        //        Sender.Tell(new ErrorResponse() { });        // Warning： the strategy is not existed
        //        return;
        //    }
        //    else
        //    {
        //        strategyActors.Remove(strategy.Id);
        //    }
        //    strategyActors.Remove(strategy.Id);
        //}

    }
}
