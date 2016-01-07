using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    /// <summary>
    /// 股票价格队列, 当价格发生变动时,发送给观察者
    /// </summary>
    public class BidCacheQueue
    {
        /// <summary>
        /// 定义一个委托类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bid"></param>
        public delegate void BidChangeHandler(object sender, TickData bid);

        /// <summary>定义一个事件</summary>
        public event BidChangeHandler OnBidChange;

        private TickData lastData;
        private Queue<TickData> queue = new Queue<TickData>();

        private String code = null;
        // private String name = null;

        public BidCacheQueue(String code)
        {
            this.code = code;
        }

        public String Code { get { return code; } }
        // public String Name {get {return name;}}

        public TickData LastData
        {
            get
            {
                if (lastData == null)
                    lastData = new TickData();
                return lastData;
            }
        }

        List<TickData> bids = new List<TickData>();
        public List<TickData> BidList
        {
            get { return this.bids; }
        }

        public void Enqueue(TickData obj)
        {
            bids.Insert(0, obj);
            queue.Enqueue(obj);
            lastData = obj;
            if (OnBidChange != null)
            {
                OnBidChange(this, obj);
            }
        }

        public TickData Dequeue()
        {
            return queue.Dequeue();
        }
    }
}
