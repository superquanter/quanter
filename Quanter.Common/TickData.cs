using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public class TickData : ICloneable
    {
        /// <summary>
        /// 每价格挂单的数据
        /// </summary>
        public class GoodsData : ICloneable
        {
            public float Price { get; set; }
            public int OrderAmount { get; set; }

            public GoodsData(float price, int amount)
            {
                Price = price;
                OrderAmount = amount;
            }

            public GoodsData DeepClone()
            {
                return (GoodsData)Clone();
            }
            public object Clone()
            {
                GoodsData data = new GoodsData(Price, OrderAmount);

                return data;
            }
        }

        public String Code { get; set; }    // 证券代码
        public string Name { get; set; }    // 名称

        public float LastClose { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }

        public float CurrentPrice { get; set; }    // 成交价
        public long Volumn { get; set; }     // 成交量
        public float Turnover { get; set; }      // 成交金额
        public String PushTime { get; set; }  // 推送时间

        private ICollection<GoodsData> buyList = new List<GoodsData>();
        private ICollection<GoodsData> sellList = new List<GoodsData>();

        public GoodsData[] BuyList
        {
            get
            {
                if (buyList.Count == 0) return new GoodsData[5] { new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0) };
                return this.buyList.ToArray<GoodsData>();
            }
        }

        public GoodsData[] SellList
        {
            get
            {
                if (sellList.Count == 0) return new GoodsData[5] { new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0), new GoodsData(0, 0) };

                return this.sellList.ToArray<GoodsData>();
            }
        }

        public void AddBuyGoodsData(GoodsData data)
        {
            this.buyList.Add(data);
        }

        public void AddSellGoodsData(GoodsData data)
        {
            this.sellList.Add(data);
        }

        public TickData DeepClone()
        {
            return (TickData)Clone();
        }
        public object Clone()
        {
            TickData bid = new TickData();
            bid.Code = Code;
            bid.High = High;
            bid.Low = Low;
            bid.Open = Open;
            bid.LastClose = LastClose;

            bid.CurrentPrice = CurrentPrice;
            bid.Turnover = Turnover;
            bid.Volumn = Volumn;
            bid.PushTime = PushTime;

            bid.buyList = new List<GoodsData>();
            buyList.AsParallel<GoodsData>().ForAll(d=>
            {
                bid.buyList.Add(d.DeepClone());
            });

            bid.sellList = new List<GoodsData>();
            sellList.AsParallel<GoodsData>().ForAll(d =>
            {
                bid.sellList.Add(d.DeepClone());
            });

            return bid;
        }
    }
}
