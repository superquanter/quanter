using Quanter.Common;
using Quanter.Domain;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy
{
    public interface IStrategy
    {
        int Id { get; }

        String Name { get; set; }

        StrategyDesc Desc { get; set; }

        TradeMode TradeMode { get; set; }

        List<Securities> SecuritiesList { get; set; }

        /// <summary>
        /// 初始化策略
        /// </summary>
        void Init();

        /// <summary>
        /// 启动策略
        /// </summary>
        void Start();

        /// <summary>
        /// 停止策略
        /// </summary>
        void Stop();

        /// <summary>
        /// Tick 数据到达
        /// </summary>
        void OnTickData();

        /// <summary>
        /// Bar数据到达
        /// </summary>
        void OnBarData();

        /// <summary>
        /// 报价数据到达
        /// </summary>
        void OnQuoteData();

        /// <summary>
        /// 外部其他因素触发
        /// </summary>
        void Run();
    }
}
