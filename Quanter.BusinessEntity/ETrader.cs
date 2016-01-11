using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.BusinessEntity
{
    public class ETrader
    {
        public virtual int Id { get; set; }

        public virtual String Username { get; set; }
        public virtual String Password { get; set; }
        public virtual String ServicePwd { get; set; }

        public virtual String Path { get; set; }
        public virtual DateTime Date { get; set; }

    }
}
