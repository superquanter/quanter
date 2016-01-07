using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using Xunit;
using Quanter.Persistence;
using Quanter.Trader.Messages;
using Quanter.BusinessEntity;

namespace Tests.Quanter.Persistence
{
    public class PersistenceActorSpec : TestKit
    {
        [Fact]
        public void save_update_load_find_delete_strategy()
        {
            TestActorRef<PersistenceActor> persistenceActorRef = ActorOfAsTestActorRef<PersistenceActor>("persistence");
            EStrategy strategy = new EStrategy() { Type = "Quanter.Strategy.Demo.DemoStrategyActor, Quanter.Strategy.Demo", Desc="测试策略", Name = "策略DEMO", Date=DateTime.Now };
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = strategy};
            persistenceActorRef.Tell(req);

        }

    }
}
