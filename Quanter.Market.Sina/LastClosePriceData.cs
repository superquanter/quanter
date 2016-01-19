using CsvHelper;
using Quanter.Common;
using slf4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Market
{
    public class LastClosePriceDataHelper
    {
        private ILogger _log = LoggerFactory.GetILoggerFactory().GetLogger("Quanter.Market.Sina.LastClosePriceDataHelper");
        private IDictionary<String, LastClosePriceData> lastClosePriceDatas = new Dictionary<String, LastClosePriceData>();

        public class LastClosePriceData
        {
            public String Symbol { get; set; }
            public String Alias
            {
                get
                {
                    String alias = string.Empty;
                    if (Symbol.EndsWith("XSHG")) alias = "sh" + Symbol.Substring(0, 6);
                    else if (Symbol.EndsWith("XSHE")) alias = "sz" + Symbol.Substring(0, 6);

                    return alias;
                }
            }
            public String Name { get; set; }
            public float Price { get; set; }

            public float BuyPrice { get; }
            public float SellPrice { get; }


        }

        static string dataurl = "http://hq.sinajs.cn/list={0}";
        static String filepath = "last_close_price.csv";
        private HttpClient client = null;

        private static LastClosePriceDataHelper _instance = new LastClosePriceDataHelper();

        public static LastClosePriceDataHelper Instance
        {
            get { return _instance; }
        }

        private LastClosePriceDataHelper() { }

        public IDictionary<String, LastClosePriceData>  LastClosePriceDatas
        {
            get { return this.lastClosePriceDatas; }
        }

        public void WriteLastClosePrice()
        {
            // 读取文件内容
            ReadLastClosePrice();

            // 从sina 取数据
            _fetchFromSina();

            // 写文件
            _writeToFile();
        }

        public void ReadLastClosePrice()
        {
            lastClosePriceDatas.Clear();

            using (StreamReader sr = new StreamReader(filepath))
            using (CsvReader reader = new CsvReader(sr))
            {
                while (reader.Read())
                {
                    String symbol = reader.GetField(0);
                    String name = reader.GetField(1);
                    float price = float.Parse(reader.GetField(2));

                    _log.Debug(" 读取价格 {0}, {1} ", symbol, price);

                    lastClosePriceDatas.Add(symbol, new LastClosePriceData() { Symbol = symbol, Name = name, Price = price });
                }
            }

        }

        private void _fetchFromSina()
        {
            client = new HttpClient();
            List<string> queryString = new List<string>();
            bool isSent = false;
            if (lastClosePriceDatas.Count != 0)
            {
                int i = 0;
                int n = 150;
                StringBuilder sb = new StringBuilder();
                foreach(var data in lastClosePriceDatas.Values)
                {
                    i++;
                    isSent = false;
                    sb.Append(data.Alias);
                    sb.Append(",");

                    // 构建query string
                    if (i % n == (n - 1))
                    {
                        sb.Remove(sb.Length - 1, 1);
                        queryString.Add(sb.ToString());
                        sb.Clear();
                        isSent = true;
                    }
                }
                if (!isSent)
                {
                    sb.Remove(sb.Length - 1, 1);
                    queryString.Add(sb.ToString());
                }
            }

            foreach (string item in queryString)
            {
                _sendRequest(item);
            }
        }

        private void _sendRequest(String s)
        {
            string resp = client.Get(String.Format(dataurl, s));
            if (resp == null)
            {
                _log.Warn("没有获取到数据 {0}", s);
                return;
            }
            string[] respData = resp.Split(new char[] { '\n' });
            for (int i = 0; i < respData.Length; i++)
            {
                if (respData[i].Length != 0)
                {
                    _parseData(respData[i]);
                }
            }
        }

        private void _parseData(String data)
        {
            String[] ary = data.Split(',');
            String symbol = _getSymbol(ary[0]);
            if (ary.Length > 4)    // 小于4 时，当天未交易。 不修改价格
            {
                lastClosePriceDatas[symbol].Name = ary[0].Substring(21);
                lastClosePriceDatas[symbol].Price = float.Parse(ary[1]);
            }
        }

        private string _getSymbol(String data)
        {
            String alias = data.Substring(11, 8);
            String symbol = null;
            if (alias.StartsWith("sz"))
            {
                symbol = alias.Substring(2, 6) + ".XSHE";
            }
            else
            {
                symbol = alias.Substring(2, 6) + ".XSHG";
            }

            return symbol;
        }

        private void _writeToFile() {
            using (TextWriter tw = new StreamWriter(filepath))
            using (CsvWriter writer = new CsvWriter(tw))
            {
                writer.WriteField("代码");
                writer.WriteField("名称");
                writer.WriteField("收盘价");
                writer.NextRecord();
                foreach (var sp in lastClosePriceDatas.Values)
                {
                    _log.Debug(" 写入价格 {0}, {1} ", sp.Symbol, sp.Price);
                    writer.WriteField(sp.Symbol);
                    writer.WriteField(sp.Name);
                    writer.WriteField(sp.Price);
                    writer.NextRecord();
                }
            }
        }


    }
}
