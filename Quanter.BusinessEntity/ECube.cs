using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.BusinessEntity
{
    public class ECube
    {
        public virtual int Id { get; set; }
        public virtual EStrategy Strategy { get; set; }     // 策略
        public virtual String Symbol { get; set; }        // 组合代码
        public virtual long PreAdjustmentId { get; set; }   //调整ID

    }
}
