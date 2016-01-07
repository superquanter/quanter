using Quanter.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{

    public class StrategiesRequest
    {
        private EStrategyList sds = new EStrategyList();
        public EStrategy[] Strategies { get { return sds.ToArray<EStrategy>(); } }

        public void AddStrategy(EStrategy sd)
        {
            sds.Add(sd);
        }

        public void RemoveStrategy(EStrategy sd)
        {
            sds.Remove(sd);
        }
    }

    public class StrategiesResponse
    {
        private EStrategyList sds = new EStrategyList();
        public EStrategy[] Strategies { get { return sds.ToArray<EStrategy>(); } }

        public void Add(EStrategy sd)
        {
            sds.Add(sd);
        }

        public void Remove(EStrategy sd)
        {
            sds.Remove(sd);
        }

    }

    public class MyStrategiesRequest : StrategiesRequest
    {
        public String Username { get; set; }

    }

    public class MyStrategiesResponse : StrategiesResponse
    {

    }

    public enum StrategyRequestType
    {
        CREATE,
        START,
        STOP,
    }
    public class StrategyRequest 
    {
        public StrategyRequestType Type { get; set; }

        public object Body { get; set; }
    }

    public class StrategyResponse
    {
        public enum ResponseType
        {
            TICK_ARRIVED,
            BAR_ARRIVED,
            QUOTE_ARRIVED,
            RUN_ARRIVED,
        }

        public ResponseType Type { get; set; }

        public object Body { get; set; }
    }

    public class StartStrateiesReponse
    {

    }

}
