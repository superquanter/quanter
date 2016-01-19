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
    public class ReadLastClosePriceJob : IJob
    {
        private ILogger _log = LoggerFactory.GetILoggerFactory().GetLogger("Quanter.Market.Jobs.ReadLastClosePriceJob");

        public void Execute(IJobExecutionContext context)
        {
            _log.Info("执行作业：读入最新的收盘价。");
            LastClosePriceDataHelper.Instance.ReadLastClosePrice();
        }
    }
}
