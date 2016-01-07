using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public interface ITrader
    {
        void Connection();

        void Login();

        void Trade();
    }
}
