using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy.Risk
{
    public class RiskManager
    {
        private readonly IList<IRiskRule> _rules = new List<IRiskRule>();

        public void Reset()
        {
            _rules.AsParallel<IRiskRule>().ForAll( r => r.Reset());
        }
    }
}
