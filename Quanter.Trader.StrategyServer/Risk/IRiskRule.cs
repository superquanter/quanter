using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy.Risk
{
    public interface IRiskRule
    {
        String Title { get; }

        RiskActions Action { get; set; }

        /// <summary>
        /// 处理单个订单，资金组合等
        /// </summary>
        /// <returns></returns>
        bool ProcessMessage(RiskMessage message);

        void Reset();
    }

    public class RiskMessage
    {
        public enum MessageType
        {
            ORDER,
        }

        public MessageType Type { get; set; }
        public Object Body { get; set; }
    }
}
