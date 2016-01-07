using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy.Risk
{
    public abstract class AbstractRiskRule : IRiskRule
    {
        public RiskActions Action { get; set; }

        public string Title { get; protected set; }

        public abstract bool ProcessMessage();
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
        public override bool ProcessMessage()
        {
            throw new NotImplementedException();
        }
    }
}
