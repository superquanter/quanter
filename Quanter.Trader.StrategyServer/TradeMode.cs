namespace Quanter.Strategy
{
    /// <summary>
    /// 支持三种交易方式， 其中回测必须的数据源必须是历史数据，下单为虚拟下单
    /// </summary>
    public enum TradeMode
    {   
        BACK_TEST,      // 回测
        MOCK_TRADE,     // 模拟交易
        AUTO_TRADE,     // 实盘自动交易
    }
}