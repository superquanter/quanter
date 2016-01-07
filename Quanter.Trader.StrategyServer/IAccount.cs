using Stock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Trader.Strategy
{
    public interface IAccount
    {

        void BuySecurities(Securities securities, float price, int amount);

        void SellSecurities(Securities securities, float price, int amount);

        void CancelSecurities(int entrustNo);


    }
}
