using Akka.Actor;
using Akka.Event;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Reflection;
using System.Configuration;
using Quanter.BusinessEntity;

namespace Quanter.Market
{
    /// <summary>
    /// 处理的消息有
    /// 1、启动市场
    /// 2、停止市场
    /// 3、增加策略
    /// 4、减少策略
    /// </summary>
    public class SecuritiesMarketManagerActor : TypedActor, IHandle<MarketRequest>
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private IDictionary<String, IActorRef> secActors = new Dictionary<String, IActorRef>();
        private List<Securities> secs = new List<Securities>();
        private IActorRef quotationActor = null;


        protected override void PreStart()
        {
            base.PreStart();
        }
        public void Handle(MarketRequest message)
        {
            try { 
                switch(message.Type)
                {
                    case MarketRequest.RequestType.INIT:
                        _initStockMarket();
                        _initMarketReceiver();
                        // _tellAskedSecurities();
                        break;
                    case MarketRequest.RequestType.START:
                        _startMarket();
                        break;
                    case MarketRequest.RequestType.ADD_SECURITIES:
                        _addNewSecurities((Securities)message.Body);
                        break;
                    case MarketRequest.RequestType.STOP:
                        _log.Info("停止证券市场服务");
                        _stopMarket();
                        break;
                    default:
                        _log.Info("不支持的请求类型 {0}", message.Type);
                        break;
                }
            }
            catch (Exception e)
            {
                _log.Error("SecuritiesMarketManagerActor.Handle<MarketRequest>发生异常：{0}", e.StackTrace);
            }

        }

        private void _startMarket()
        {
            _log.Info("通知行情接收器Actor，开始接收行情数据");
            quotationActor.Tell(new QuotationRequest() { Type = QuotationRequest.RequestType.RUN });
        }

        private void _stopMarket()
        {
            // 关闭所有的Actor
            foreach(var key in secActors.Keys)
            {
                _log.Debug("关闭Symbol {0} Actor", key);
                secActors[key].GracefulStop(TimeSpan.FromSeconds(5));
            }
            secActors.Clear();

            // quotationActor.Tell("");    // 关闭接收数据
            _log.Debug("关闭行情接收器 Actor");
            quotationActor.GracefulStop(TimeSpan.FromSeconds(5));
        }

        private void _addNewSecurities (Securities sec)
        {
            //if (!secActors.ContainsKey(sec.Symbol))
            //{
            //    _log.Debug("创建一个新关注的股票 {0}Actor", sec.Symbol);
            //    var secActor = Context.ActorOf(Props.Create<SecuritiesQuotationActor>(sec), sec.Symbol);
            //    secActors.Add(sec.Symbol, secActor);
            //    //secs.Add(sec);

                _tellAskedSecurities(sec);
            //}
        }

        /// <summary>
        /// 初始化证券报价列表
        /// </summary>
        private void _initStockMarket()
        {
            _log.Info("初始化证券市场关注的证券列表");
            secActors.Clear();
            secs.Clear();

            // 读取证券列表
            string filePath = "stock_list.csv";
            CsvReader reader = new CsvReader(new StreamReader(filePath));
            while (reader.Read())
            {
                String code = reader.GetField(0);
                MarketType mt = (MarketType)Enum.Parse(typeof(MarketType), reader.GetField(1));
                String name = reader.GetField(2);
                Securities sec = new Securities(SecuritiesTypes.Stock, mt, code);
                try {
                    if (secActors.ContainsKey(sec.Symbol))
                    {
                        _log.Error("发现重复证券代码 {0}", sec.Symbol);
                    }
                    else
                    {
                        var secActor = Context.ActorOf(Props.Create<SecuritiesQuotationActor>(sec), sec.Symbol);
                        secActors.Add(sec.Symbol, secActor);
                        secs.Add(sec);
                        _log.Debug("创建了{0}'s Actor, 路径是 {1}", sec.Symbol, secActor.Path);
                    }
                } catch(Exception e)
                {
                    _log.Error("创建了{0}'s Actor, 发生异常: {1}", sec.Symbol, e.StackTrace);
                }
            }
        }

        /// <summary>
        /// 动态加载市场行情接收
        /// </summary>
        private void _initMarketReceiver()
        {
            try {
                //MarketReceiverConfigurationSection config = (MarketReceiverConfigurationSection)ConfigurationManager.GetSection("receivers");
                //foreach(var receiver in config.Receivers)
                //{
                //    ReceiverElement rec = ((ReceiverElement)receiver);
                //    Type t1 = Type.GetType(rec.Type);
                //    var secActor1 = Context.ActorOf(Props.Create(t1), rec.Name);
                //}
                Type t = Type.GetType("Quanter.Market.Sina.SinaQuotationActor, Quanter.Market.Sina");
                quotationActor = Context.ActorOf(Props.Create(t), "sina.quotation");

                // 通知这个actor 接收那些股票行情
                //quotationActor.Tell("");
                _log.Debug("成功创建{0}市场行情接收器", "sina.quotation");
            }
            catch (Exception e)
            {
                _log.Error("市场行情接收创建失败 {0}", e.StackTrace);
            }
            
        }

        private void _tellAskedSecurities()
        {
            QuotationRequest req = new QuotationRequest()
            {
                Type = QuotationRequest.RequestType.ASKED_SECURITIES,
                Body = secs
            };
            quotationActor.Tell(req);
        }

        private void _tellAskedSecurities(Securities sec)
        {
            List<Securities> t = new List<Securities>();
            t.Add(sec);
            QuotationRequest req = new QuotationRequest()
            {
                Type = QuotationRequest.RequestType.ASKED_SECURITIES,
                Body = t
            };

            quotationActor.Tell(req);

        }
    }
}
