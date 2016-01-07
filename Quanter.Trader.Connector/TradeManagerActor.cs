using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    class TradeManagerActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            throw new NotImplementedException();
        }

        private void _createTradeActor()
        {
            // 读取数据库， exe的位置，交易软件ths|tdx

            //
        }
    }
}
