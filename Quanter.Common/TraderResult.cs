using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public enum TraderResultEnum
    {
        SUCCESS,    // 成功
        ERROR,      // 错误
        UNLOGIN,    // 未登录
        TIMEOUT     // 超时
    }

    public class TraderResult
    {
        /// <summary>
        /// -1: 表示调用不成功
        /// </summary>
        public TraderResultEnum Code { get; set; }
        public string Message { get; set; }
        public int EntrustNo { get; set; }
        public object Result { get; set; }

        public TraderResult()
        {
            Code = TraderResultEnum.TIMEOUT;
        }
    }
}
