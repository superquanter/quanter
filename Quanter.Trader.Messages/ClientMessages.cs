using Quanter.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{
    public enum RequestType : int
    {
        LOGIN,                  // 登录
        START_MY_STRATEGIES,    // 启动
        STOP_MY_STRATEGIES,     // 停止
        PAUSE,                  // 暂停
        RESTORE,                // 恢复
        GET_ALL_STRATEGIES,     // 获取所有策略列表
        GET_MY_STRATEGIES,      // 获取我的策略
        ADD_MY_STRATEGY,        // 增加我的策略
        REMOVE_MY_STRATEGY,     // 移除我的策略
        SETUP_STRATEGY,         // 配置我的策略
        QUERY_STRATEGY_ACCOUNT,     // 获取指定策略的账户信息
        UPDATE_ACCOUNT,             // 更新账户
        START_MARKET,
        STOP_MARKET
    }

    public enum ResponseType : int
    {
        RETURN_ALL_STRATEGIES,
        RETURN_MY_STRATEGIES,
        RETURN_LOGIN_INFO,
    }

    public class ClientRequest
    {
        public RequestType? Action { get; set; }

        public int Userid { get; set; }

        public object Body { get; set; }

    }

    public class ClientResponse
    {
        public ResponseType Action { get; set; }

        public Object Body { get; set; }
    }

    public class EStrategyList : List<EStrategy>
    {

    }
}
