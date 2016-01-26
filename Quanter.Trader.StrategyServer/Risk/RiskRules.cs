using CsvHelper;
using Quanter.Trader.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy.Risk
{
    public abstract class AbstractRiskRule : IRiskRule
    {
        public RiskActions Action { get; set; }

        public string Title { get; protected set; }

        public abstract bool ProcessMessage(RiskMessage message);

        public void Reset()
        {
            
        }
    }

    public class PnLRiskRule : AbstractRiskRule
    {
        private decimal pnL;
        public decimal PnL
        {
            get { return this.pnL; }
            set {
                this.pnL = value;
                Title = pnL.ToString();
            }
        }
        public override bool ProcessMessage(RiskMessage message)
        {
            throw new NotImplementedException();
        }
    }

    public class BlankListRiskRule : AbstractRiskRule
    {
        private IList<String> symbols = new List<String>();

        public BlankListRiskRule(String file)
        {
            _init(file);
            this.Title = "黑名单管理";
        }

        private void _init(String file)
        {
            using (StreamReader sr = new StreamReader(file))
            using (CsvReader reader = new CsvReader(sr))
            {
                while (reader.Read())
                {
                    String symbol = reader.GetField(0);
                    AddBlankList(symbol);
                }
            }
        }

        public void AddBlankList(String symbol)
        {
            symbols.Add(symbol);
        }

        public override bool ProcessMessage(RiskMessage message)
        {
            bool ret = false;
            switch (message.Type)
            {
                case RiskMessage.MessageType.ORDER:
                    ret = _processOrder((Order)message.Body);
                    break;
                default:
                    break;
            }
            return ret;
        }

        private bool _processOrder(Order order)
        {
            if (symbols.Contains(order.Symbol))
            {
                Action = RiskActions.CancelOrder;
                return false;
            }

            return true;

        }
    }
}
