using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Domain
{
    /// <summary>
    /// 策略的种类
    /// </summary>
    public class StrategyDesc
    {
        public virtual int Id { get; set; }
        public virtual String Dll { get; set; }
        public virtual String Clazz { get; set; }
        public virtual String Name { get; set; }
        public virtual String Desc { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool Enabled { get; set; }

    }
}
