using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Strategy.Risk
{
    public interface IRiskRule
    {
        String Title { get; }

        RiskActions Action { get; set; }

        /// <summary>
        /// 处理订单
        /// </summary>
        /// <returns></returns>
        bool ProcessMessage();
    }
}
