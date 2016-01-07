using Akka.Actor;
using Quanter.BusinessEntity;
using Quanter.Market;
using Quanter.Persistence;
using Quanter.Strategy;
using Quanter.Trader.Messages;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Quanter.Trader.Gui
{
    public partial class MainForm : Form
    {

        private event EventHandler<StrategiesResponse> strategiesDataAvailable;    // 所有的策略列表，我的策略列表，

        private ActorSystem actorSystem;
        private IActorRef persistenceActor;
        private IActorRef strategyActor;
        private IActorRef marketActor;
        private IActorRef userActor;

        public MainForm()
        {
            InitializeComponent();

            //actorSystem = ActorSystem.Create("strategies");
            //var clientActor = actorSystem.ActorOf(Props.Create<StockTraderClientActor>());

            // 登录
            //ClientRequest loginReq = new ClientRequest()
            //{
            //    Action = RequestType.LOGIN,
            //    Body = new User() { Username = "joe", Password = "123456" }
            //};
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            //var resp0 = clientActor.Ask<ClientResponse>(loginReq, TimeSpan.FromSeconds(15));
            //resp0.Wait();
            //Console.WriteLine("返回:{0}, 耗时:{1}", resp0.Result.Body, watch.ElapsedMilliseconds);

            //// 获取所有的策略列表
            //var resp = clientActor.Ask<ClientResponse>(new ClientRequest() { Action = RequestType.GET_ALL_STRATEGIES }, TimeSpan.FromSeconds(5));
            //resp.Wait();
            //StrategiesResponse rep = (StrategiesResponse)(resp.Result.Body);
            //rep.Strategies.AsParallel<StrategyDesc>().ForAll<StrategyDesc>(sd => Console.WriteLine("策略 {0}", sd.Name));

            // 启动行情
            //clientActor.Tell(new ClientRequest() { Action = RequestType.START_MARKET });

            //// 获取我的策略
            //clientActor.Tell(new ClientRequest() { Action = RequestType.GET_MY_STRATEGIES });

            //// 启动我的策略
            //clientActor.Tell(new ClientRequest() { Action = RequestType.START_MY_STRATEGIES });
            //Thread.Sleep(1000);
            //clientActor.Tell(new ClientRequest() { Action = RequestType.STOP_MY_STRATEGIES });

            //// 停止行情
            //clientActor.Tell(new ClientRequest() { Action = RequestType.STOP_MARKET });
            btnInit_Click(null, null);
        }

        private void _init()
        {
            actorSystem = ActorSystem.Create("strategiesServer");

            strategyActor = actorSystem.ActorOf(Props.Create<StrategyManagerActor>(), ConstantsHelper.AKKA_PATH_STRATEGY_MANAGER);
            persistenceActor = actorSystem.ActorOf(Props.Create<PersistenceActor>(), ConstantsHelper.AKKA_PATH_PERSISTENCE);
            marketActor = actorSystem.ActorOf(Props.Create<SecuritiesMarketManagerActor>(), ConstantsHelper.AKKA_PATH_MARKET_MANAGER);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            actorSystem.Shutdown();
            base.OnClosing(e);
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _init();
        }

        private void btnStartMarket_Click(object sender, EventArgs e)
        {
            marketActor.Tell(new MarketRequest() { Type = MarketRequest.RequestType.START });
        }

        private void btnStopMarket_Click(object sender, EventArgs e)
        {
            marketActor.Tell(new MarketRequest() { Type = MarketRequest.RequestType.STOP });
        }

        private void btnStartStrategy_Click(object sender, EventArgs e)
        {

        }

        private void btnRegStrategy_Click(object sender, EventArgs e)
        {
            // Load一个新的策略实例
            EStrategy strategy1 = new EStrategy()
            {
                Id = 1,
                Type = "Quanter.Strategy.Demo.DemoStrategyActor, Quanter.Strategy.Demo",
                Name= "Demo示例",
                Desc = "Demo实例"
            };

            EStrategy strategy2 = new EStrategy()
            {
                Id = 2,
                Type = "Quanter.Strategy.XueQiuStrategy.TraceXueQiuStrategyActor, Quanter.Strategy.XueQiuStrategy",
                Name = "雪球策略",
                Desc = "雪球实例"
            };

            EStrategy strategy3 = new EStrategy()
            {
                Id = 3,
                Type = "Quanter.Strategy.RationBStrategyActor, Quanter.Strategy.RationB",
                Name = "分级策略",
                Desc = "雪球实例"
            };

            StrategyRequest req = new StrategyRequest() { Type = StrategyRequestType.CREATE, Body = strategy1 };
            strategyActor.Tell(req);
            //req = new StrategyRequest() { Type = StrategyRequestType.CREATE, Body = strategy2 };
            //strategyActor.Tell(req);
            //req = new StrategyRequest() { Type = StrategyRequestType.CREATE, Body = strategy3 };
            //strategyActor.Tell(req);

        }
    }

}
