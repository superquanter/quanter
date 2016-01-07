using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public delegate void TurnoverReturnHandler(int entrustNo, String code, float price, int amount);
    public interface IStockTrader
    {
        event TurnoverReturnHandler OnTurnoverReturn;
        void Init();

        /// <summary>
        /// 卖股票
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="num"></param>
        /// <returns>合同号</returns>
        TraderResult SellStock(String code, float price, int num);

        /// <summary>
        /// 买股票
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="num"></param>
        /// <returns>合同号</returns>
        TraderResult BuyStock(String code, float price, int num);

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="entrustNo">合约号</param>
        /// <returns>合同号</returns>
        TraderResult CancelStock(int entrustNo);

        /// <summary>
        /// 获取成交信息
        /// </summary>
        TraderResult GetTodayTradeList();

        /// <summary>
        /// 获取成交信息
        /// </summary>
        TraderResult GetTodayEntrustList();

        /// <summary>
        /// 保持连接
        /// </summary>
        void Keep();

        /// <summary>
        /// 获取资金信息
        /// </summary>
        TraderResult GetTradingAccountInfo();
    }
}
