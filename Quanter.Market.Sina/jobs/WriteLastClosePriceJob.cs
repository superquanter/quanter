using Quanter.Market.Sina;
using Quartz;
using slf4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Market.Jobs
{
    public class WriteLastClosePriceJob : IJob
    {
        private ILogger _log = LoggerFactory.GetILoggerFactory().GetLogger("Quanter.Market.Jobs.WriteLastClosePriceJob");

        public void Execute(IJobExecutionContext context)
        {
            _log.Info("执行作业：更新最新的收盘价。");
            LastClosePriceDataHelper.Instance.WriteLastClosePrice();
        }
    }
}
