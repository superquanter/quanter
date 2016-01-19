using Akka.Actor;
using Quanter.Trader.Messages;
using Quartz;
using slf4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Market.Jobs
{
    public class FetchSinaQuoteDataJob : IJob
    {
        private ILogger _log = LoggerFactory.GetILoggerFactory().GetLogger("Quanter.Market.Jobs.FetchSinaQuoteDataJob");
        ActorSystem actorSystem = null;
        ActorSelection sinaActor = null;

        public FetchSinaQuoteDataJob()
        {
            _init();
        }

        private void _init()
        {
            _log.Debug("初始化监听sina的作业");
            actorSystem = ActorSystem.Create(ConstantsHelper.AKKA_PATH_SERVER);
            // var path = String.Format("akka.tcp://{2}@localhost:8091/user/{0}/{1}", ConstantsHelper.AKKA_PATH_MARKET_MANAGER, "sina.quotation", ConstantsHelper.AKKA_PATH_SERVER);
            var path = String.Format("/user/{0}/{1}", ConstantsHelper.AKKA_PATH_MARKET_MANAGER, "sina.quotation");
            sinaActor = actorSystem.ActorSelection(path);
        }

        public void Execute(IJobExecutionContext context)
        {
            _log.Debug("执行监听sina的作业");
            QuotationRequest req = new QuotationRequest() { Type = QuotationRequest.RequestType.RUN };
            sinaActor.Tell(req);
        }
    }
}
