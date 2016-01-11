using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{
    public enum PersistenceType
    {
        INIT_DATABASE,
        OPEN,
        CLOSE,
        SAVE,
        UPDATE,
        DELETE,
        LOAD,
        FIND,
        LIST,
    }
    public class PersistenceRequest
    {
        public PersistenceType Type { get; set; }
        public Object Body { get; set; }
    }
}
