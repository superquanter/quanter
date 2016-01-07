using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual String Username { get; set; }
        public virtual String Password { get; set; }
    }
}
