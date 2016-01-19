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
using Akka.Actor;

namespace Tests.Quanter.Persistence
{
    public class PersistenceActorSpec : TestKit
    {
        TestActorRef<PersistenceActor> persistenceActorRef = null;

        public PersistenceActorSpec()
        {
            persistenceActorRef = ActorOfAsTestActorRef<PersistenceActor>("persistence");
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.INIT_DATABASE };
            persistenceActorRef.Tell(req);
            req = new PersistenceRequest() { Type = PersistenceType.OPEN };
            persistenceActorRef.Tell(req);

        }

        [Fact]
        public void save_update_load_find_delete_strategy()
        {
            ETrader trader = new ETrader() { Username = "012345678", Password = "12345678", ServicePwd = "012345678", Path = "c:/xiadan/xiadan.exe", Date = DateTime.Now };
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = trader };
            persistenceActorRef.Tell(req);
            var result = ExpectMsg<ETrader>();

            EStrategy strategy = new EStrategy() { Type = "Quanter.Strategy.Demo.DemoStrategyActor, Quanter.Strategy.Demo", Desc="测试策略", Name = "策略DEMO", EnableBalance=50000, FrozenBalance =0, Enabled = true, Date=DateTime.Now, Trader = result };
            req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = strategy};
            persistenceActorRef.Tell(req);
            var result1 = ExpectMsg<EStrategy>();
            Assert.NotEqual(0, result1.Id);

            req = new PersistenceRequest() { Type = PersistenceType.FIND, Body = "from EStrategy where Id=1" };
            persistenceActorRef.Tell(req);
            var result2 = ExpectMsg<EStrategy>();
            Assert.Equal(1, result2.Id);
            Assert.Equal("012345678", result2.Trader.Username);

        }

        [Fact]
        public void save_update_load_find_delete_trader()
        {
            ETrader trader = new ETrader() { Username = "1234567", Password="12345678", ServicePwd="12345678", Path="c:/xiadan/xiadan.exe", Date = DateTime.Now };
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = trader };
            persistenceActorRef.Tell(req);
            var result0 = ExpectMsg<ETrader>();
            Assert.NotEqual(0, result0.Id);
            Assert.Equal("1234567", result0.Username);

            trader = new ETrader() { Username = "2345678", Password = "12345678", ServicePwd = "12345678", Path = "c:/xiadan/xiadan.exe", Date = DateTime.Now };
            req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = trader };
            persistenceActorRef.Tell(req);
            var result1 = ExpectMsg<ETrader>();
            Assert.NotEqual(0, result1.Id);
            Assert.Equal("2345678", result1.Username);

            req = new PersistenceRequest() { Type = PersistenceType.FIND, Body = "from ETrader where Id=1" };
            persistenceActorRef.Tell(req);
            var result2 = ExpectMsg<ETrader>();
            Assert.Equal(1, result2.Id);
            Assert.Equal("1234567", result2.Username);

        }

        [Fact]
        public void save_update_load_find_delete_cube()
        {
            PersistenceRequest req = new PersistenceRequest() { Type = PersistenceType.LIST, Body = "from ECube" };
            persistenceActorRef.Tell(req);
            var result0 = ExpectMsg<List<object>>();

            Assert.Equal(0, result0.Count);

            // 先保存Trader
            ETrader trader = new ETrader() { Username = "012345678", Password = "12345678", ServicePwd = "012345678", Path = "c:/xiadan/xiadan.exe", Date = DateTime.Now };
             req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = trader };
            persistenceActorRef.Tell(req);
            var result = ExpectMsg<ETrader>();

            // 保存strategy
            EStrategy strategy = new EStrategy() { Type = "Quanter.Strategy.XueQiuStrategy.TraceXueQiuStrategyActor, Quanter.Strategy.XueQiuStrategy", Desc = "测试策略", Name = "组合策略DEMO", EnableBalance = 50000, FrozenBalance = 0, Enabled = true, Date = DateTime.Now, Trader = null };
            EStockHolder holder = new EStockHolder() { Strategy = strategy, Code = "000001", Symbol = "000001.XSHE", Name = "平安银行", CostPrice = 11.03f, LastPrice = 12.0f, IncomeAmount = 1000, EnableAmount = 1000 };
            EStockHolder holder1 = new EStockHolder() { Strategy = strategy, Code = "000002", Symbol = "000002.XSHE", Name = "万科A", CostPrice = 9.03f, LastPrice = 12.0f, IncomeAmount = 1000, EnableAmount = 1000 };
            strategy.Holders.Add(holder);
            strategy.Holders.Add(holder1);


            ECube cube = new ECube()
            {
                Strategy = strategy,
                Symbol = "ZH000003",
                PreAdjustmentId = 0,
            };

            req = new PersistenceRequest() { Type = PersistenceType.SAVE, Body = cube };
            persistenceActorRef.Tell(req);
            var result2 = ExpectMsg<ECube>();
            Assert.NotEqual(0, result2.Id);
            Assert.Equal(result2.Symbol, "ZH000003");

            req = new PersistenceRequest() { Type = PersistenceType.LIST, Body = "from ECube" };
            persistenceActorRef.Tell(req);
            var result3 = ExpectMsg<List<object>>();

            Assert.NotEqual(0, result3.Count);
            Assert.Equal("ZH000003", ((ECube)result3[0]).Symbol); 
        }

    }
}
