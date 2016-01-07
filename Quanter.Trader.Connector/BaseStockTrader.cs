using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public abstract class BaseStockTrader : IStockTrader
    {
        public event TurnoverReturnHandler OnTurnoverReturn;

        protected void StockTrader_OnTurnOverReturn(int entrustNo, String code, float price, int amount)
        {
            if (OnTurnoverReturn != null)
                OnTurnoverReturn(entrustNo, code, price, amount);
        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        protected virtual void Login()
        {
        }

        public TraderResult SellStock(string code, float price, int num)
        {
            lock (this)
            {
                TraderResult ret = internalSellStock(code, price, num);
                switch (ret.Code)
                {
                    case TraderResultEnum.TIMEOUT:
                        return SellStock(code, price, num);
                    case TraderResultEnum.UNLOGIN:
                        Login();
                        return SellStock(code, price, num);
                    case TraderResultEnum.ERROR:
                    case TraderResultEnum.SUCCESS:
                        return ret;
                    default:
                        return null;
                }
            }
        }

        protected virtual TraderResult internalSellStock(string code, float price, int num)
        {
            return null;
        }

        public TraderResult BuyStock(string code, float price, int num)
        {
            lock (this)
            {
                TraderResult ret = internalBuyStock(code, price, num);
                switch (ret.Code)
                {
                    case TraderResultEnum.TIMEOUT:
                        return BuyStock(code, price, num);
                    case TraderResultEnum.UNLOGIN:
                        Login();
                        return BuyStock(code, price, num);
                    case TraderResultEnum.ERROR:
                    case TraderResultEnum.SUCCESS:
                        return ret;
                    default:
                        return null;
                }
            }
        }

        protected virtual TraderResult internalBuyStock(string code, float price, int num)
        {
            return null;
        }

        public TraderResult CancelStock(int entrustNo)
        {
            TraderResult ret = internalCancelStock(entrustNo);
            switch (ret.Code)
            {
                case TraderResultEnum.TIMEOUT:
                    return CancelStock(entrustNo);
                case TraderResultEnum.UNLOGIN:
                    Login();
                    return CancelStock(entrustNo);
                case TraderResultEnum.ERROR:
                case TraderResultEnum.SUCCESS:
                    return ret;
                default:
                    return null;
            }
        }

        protected virtual TraderResult internalCancelStock(int entrustNo)
        {
            return null;
        }

        public TraderResult GetTodayTradeList()
        {
            TraderResult ret = internalGetTodayTradeList();
            switch (ret.Code)
            {
                case TraderResultEnum.TIMEOUT:
                    return GetTodayTradeList();
                case TraderResultEnum.UNLOGIN:
                    Login();
                    return GetTodayTradeList();
                case TraderResultEnum.ERROR:
                case TraderResultEnum.SUCCESS:
                    return ret;
                default:
                    return null;
            }
        }

        protected virtual TraderResult internalGetTodayTradeList()
        {
            return null;
        }

        public TraderResult GetTodayEntrustList()
        {
            TraderResult ret = internalGetTodayEntrustList();
            switch (ret.Code)
            {
                case TraderResultEnum.TIMEOUT:
                    return GetTodayEntrustList();
                case TraderResultEnum.UNLOGIN:
                    Login();
                    return GetTodayEntrustList();
                case TraderResultEnum.ERROR:
                case TraderResultEnum.SUCCESS:
                    return ret;
                default:
                    return null;
            }
        }

        protected virtual TraderResult internalGetTodayEntrustList()
        {
            return null;
        }

        public void Keep()
        {
            internalKeep();
        }

        protected virtual void internalKeep()
        {
            return;
        }

        public TraderResult GetTradingAccountInfo()
        {
            TraderResult ret = internalGetTradingAccountInfo();
            switch (ret.Code)
            {
                case TraderResultEnum.TIMEOUT:
                    return GetTradingAccountInfo();
                case TraderResultEnum.UNLOGIN:
                    Login();
                    return GetTradingAccountInfo();
                case TraderResultEnum.ERROR:
                case TraderResultEnum.SUCCESS:
                    return ret;
                default:
                    return null;
            }
        }

        protected virtual TraderResult internalGetTradingAccountInfo()
        {
            return null;
        }
    }
}
