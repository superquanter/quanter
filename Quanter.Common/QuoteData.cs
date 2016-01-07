using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public class QuoteData
    {
        //public class GoodsData : ICloneable
        //{
        //    public float Price { get; set; }
        //    public int OrderAmount { get; set; }

        //    public GoodsData(float price, int amount)
        //    {
        //        Price = price;
        //        OrderAmount = amount;
        //    }

        //    public GoodsData DeepClone()
        //    {
        //        return (GoodsData)Clone();
        //    }
        //    public object Clone()
        //    {
        //        GoodsData data = new GoodsData(Price, OrderAmount);

        //        return data;
        //    }
        //}

            public String Symbol { get; set; }  // 证券代码 国际标准
        public String Alias { get; set; }  // 证券代码
        public string Name { get; set; }    // 名称

        public float LastClose { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }

        public float CurrentPrice { get; set; }    // 成交价
        public long Volume { get; set; }     // 成交量
        public float Turnover { get; set; }      // 成交金额
        public String PushTime { get; set; }  // 推送时间

        //private ICollection<GoodsData> buyList = new List<GoodsData>();
        //private ICollection<GoodsData> sellList = new List<GoodsData>();

        //public GoodsData[] BuyList
        //{
        //    get
        //    {
        //        if (buyList.Count == 0) return new GoodsData[5] { new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0) };
        //        return this.buyList.ToArray<GoodsData>();
        //    }
        //}

        //public GoodsData[] SellList
        //{
        //    get
        //    {
        //        if (sellList.Count == 0) return new GoodsData[5] { new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0) };

        //        return this.sellList.ToArray<GoodsData>();
        //    }
        //}

        #region 买卖5档
        public long SellVolume { get; set; }
        public long BuyVolume { get; set; }
        public float SellPrice1 { get; set; }
        public long SellVolume1 { get; set; }
        public float SellPrice2 { get; set; }
        public long SellVolume2 { get; set; }
        public float SellPrice3 { get; set; }
        public long SellVolume3 { get; set; }
        public float SellPrice4 { get; set; }
        public long SellVolume4 { get; set; }
        public float SellPrice5 { get; set; }
        public long SellVolume5 { get; set; }
        public float BuyPrice1 { get; set; }
        public long BuyVolume1 { get; set; }
        public float BuyPrice2 { get; set; }
        public long BuyVolume2 { get; set; }
        public float BuyPrice3 { get; set; }
        public long BuyVolume3 { get; set; }
        public float BuyPrice4 { get; set; }
        public long BuyVolume4 { get; set; }
        public float BuyPrice5 { get; set; }
        public long BuyVolume5 { get; set; }
        #endregion

        public float PE { get; set; }
        public float Amplitude { get; set; }
        public float HighLimit { get; set; }
        public float LowLimit { get; set; }

        //public void AddBuyGoodsData(GoodsData data)
        //{
        //    this.buyList.Add(data);
        //}

        //public void AddSellGoodsData(GoodsData data)
        //{
        //    this.sellList.Add(data);
        //}

        //public TickData DeepClone()
        //{
        //    return (TickData)Clone();
        //}
        //public object Clone()
        //{
        //    QuoteData bid = new QuoteData();
        //    bid.Symbol = Symbol;
        //    bid.High = High;
        //    bid.Low = Low;
        //    bid.Open = Open;
        //    bid.LastClose = LastClose;

        //    bid.CurrentPrice = CurrentPrice;
        //    bid.Turnover = Turnover;
        //    bid.Volumn = Volumn;
        //    bid.PushTime = PushTime;

        //    bid.buyList = new List<GoodsData>();
        //    buyList.AsParallel<GoodsData>().ForAll(d =>
        //    {
        //        bid.buyList.Add(d.DeepClone());
        //    });

        //    bid.sellList = new List<GoodsData>();
        //    sellList.AsParallel<GoodsData>().ForAll(d =>
        //    {
        //        bid.sellList.Add(d.DeepClone());
        //    });

        //    return bid;
        //}
    }
}
