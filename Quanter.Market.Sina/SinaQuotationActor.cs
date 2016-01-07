using Quanter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using Quanter.Trader.Messages;

namespace Quanter.Market.Sina
{
    public class SinaQuotationActor : BaseQuotationActor
    {        
        private CookieContainer Cookie = new CookieContainer();

        private const string dataurl = "http://hq.sinajs.cn/list={0}";
        private HttpClient client = null;
        private List<string> symbols = new List<string>();

        protected override void PreStart()
        {
            client = new HttpClient();
        }
        protected override void run()
        {
            _run();
        }

        private List<string> t_ReqSymbols = new List<string>();

        private void _run()
        {
            _log.Info("sina level1 行情接收器开始运行");
            int n = 150;
            bool isSent = false;
            while (true)
            {
                if (aliases.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < aliases.Count; i++)
                    {
                        isSent = false;
                        sb.Append(aliases[i]);
                        sb.Append(",");

                        if (i % n == (n - 1))
                        {
                            sb.Remove(sb.Length - 1, 1);
                            t_ReqSymbols.Add(sb.ToString());
                            sb.Clear();
                            isSent = true;
                        }
                    }
                    if (!isSent)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        t_ReqSymbols.Add(sb.ToString());
                    }
                }

                foreach (string item in t_ReqSymbols)
                {
                    _sendRequest(item);
                }

                Thread.Sleep(2500);
            }
        }

        private void _sendRequest(String s)
        {
            string resp = client.Get(String.Format(dataurl, s));
            if (resp == null)
            {
                _log.Warning("没有获取到数据 {0}", s);
                return;
            }
            string[] respData = resp.Split(new char[] { '\n' });
            for (int i = 0; i < respData.Length; i++)
            {
                if (respData[i].Length != 0)
                {
                    QuoteData qdata = _parseQuoteData(respData[i]);
                    newQuoteDataArrived(qdata);
                }
            }
        }

        private QuoteData _parseQuoteData(string data)
        {
            String[] items = data.Split(new char[] { ',' });
            if (items.Length < 10) return null;

            QuoteData bid = new QuoteData();

            bid.Alias = data.Substring(11, 8);
            if (bid.Alias.StartsWith("sz")) {
                bid.Symbol = bid.Alias.Substring(2, 6) + ".XSHE";
            } else
            {
                bid.Symbol = bid.Alias.Substring(2, 6) + ".XSHG";
            }
            bid.Name = items[0].Substring(21, items[0].Length - 21);    // var hq_str_sz150023="深成指B
            bid.Open = float.Parse(items[1]);
            bid.LastClose = float.Parse(items[2]);
            bid.CurrentPrice = float.Parse(items[3]);
            bid.High = float.Parse(items[4]);
            bid.Low = float.Parse(items[5]);
            //bid.Buy = decimal.Parse(items[6]);
            //bid.Sell = decimal.Parse(items[7]);
            bid.Volume = long.Parse(items[8]);
            bid.Turnover = float.Parse(items[9]);

            //bid.AddBuyGoodsData(new TickData.GoodsData(float.Parse(items[BUY_1_P]), int.Parse(items[BUY_1_A])));
            //bid.AddBuyGoodsData(new TickData.GoodsData(float.Parse(items[BUY_2_P]), int.Parse(items[BUY_2_A])));
            //bid.AddBuyGoodsData(new TickData.GoodsData(float.Parse(items[BUY_3_P]), int.Parse(items[BUY_3_A])));
            //bid.AddBuyGoodsData(new TickData.GoodsData(float.Parse(items[BUY_4_P]), int.Parse(items[BUY_4_A])));
            //bid.AddBuyGoodsData(new TickData.GoodsData(float.Parse(items[BUY_5_P]), int.Parse(items[BUY_5_A])));


            //bid.AddSellGoodsData(new TickData.GoodsData(float.Parse(items[SELL_1_P]), int.Parse(items[SELL_1_A])));
            //bid.AddSellGoodsData(new TickData.GoodsData(float.Parse(items[SELL_2_P]), int.Parse(items[SELL_2_A])));
            //bid.AddSellGoodsData(new TickData.GoodsData(float.Parse(items[SELL_3_P]), int.Parse(items[SELL_3_A])));
            //bid.AddSellGoodsData(new TickData.GoodsData(float.Parse(items[SELL_4_P]), int.Parse(items[SELL_4_A])));
            //bid.AddSellGoodsData(new TickData.GoodsData(float.Parse(items[SELL_5_P]), int.Parse(items[SELL_5_A])));

            bid.PushTime = items[31];
            return bid;
        }

    }
}
