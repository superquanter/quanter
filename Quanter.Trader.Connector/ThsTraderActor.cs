using Akka.Actor;
using Akka.Event;
using Quanter.Common;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    
    public class ThsTraderActor : TypedActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private IStockTrader trader = new ThsStockTrader();

        /// <summary>
        /// 限价买入
        /// </summary>
        private void _buy( Order order, Securities sec)
        {
            _log.Debug("保存到策略账户 委买单 策略ID:{0}, 代码:{1}, 证券类别: {2}, 价格:{3}， 数量:{4}",order.StrategyId, order.Symbol, sec.Type, order.Price, order.Amount);

        }

        /// <summary>
        /// 限价卖出
        /// </summary>
        private void _sell(Order order, Securities sec)
        {
            _log.Debug("保存到策略账户 委卖单 策略ID:{0}, 代码:{1}, 证券类别: {2}, 价格:{3}， 数量:{4}", order.StrategyId, order.Symbol, sec.Type, order.Price, order.Amount);
        }

        /// <summary>
        /// 获取持仓信息
        /// </summary>
        private void _getPositionInfo()
        {

        }

        /// <summary>
        ///  获取资金信息
        /// </summary>
        private void _getFundInfo()
        {

        }

        /// <summary>
        /// 保持在线
        /// </summary>
        private void _keep()
        {

        }
    }
}
