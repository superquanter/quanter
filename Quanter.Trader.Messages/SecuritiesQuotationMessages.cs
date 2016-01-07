using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanter.Common;

namespace Quanter.Trader.Messages
{
    public class QuotationRequest
    {
        public enum RequestType
        {
            ASKED_SECURITIES,   // 请求接收行情的证券列表
            RUN,                // 开始接收行情
        }

        public RequestType Type { get; set; }
        public Object Body { get; set; }
    }

    public class SecuritiesQuotationRequest
    {
        public enum RequestType
        {

            WATCH_QUOTEDATA,
            WATCH_TICKDATA,
            WATCH_BARDATA,
            UNWATCH,
            NEW_TICKDATA,
            NEW_BARDATA,
            NEW_QUOTEDATA,
        }

        public RequestType Type { get; set; }

        public Object Body { get; set; }
    }
}
