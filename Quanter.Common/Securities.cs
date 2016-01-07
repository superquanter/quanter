using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    [Serializable]
    public class Securities : ISerializable
    {
        public SecuritiesTypes Type { get; private set; }
        public MarketType MarketType { get; private set; }

        public String Symbol { get; private set; }
        public String Alias {
            get
            {
                String alias = string.Empty;
                switch(MarketType)
                {
                    case MarketType.XSHG:
                        alias = "sh" + Symbol.Substring(0,6);
                        break;
                    case MarketType.XSHE:
                        alias = "sz" + Symbol.Substring(0, 6);
                        break;
                    default:
                        break;
                }

                return alias;
            }
        }

        public Securities(SecuritiesTypes secType, MarketType marketType, String code)
        {
            this.Type = secType;
            this.MarketType = marketType;
            this.Symbol = code + "." + marketType.ToString();
        }

        public Securities(SecuritiesTypes secType, String symbol)
        {
            this.Type = secType;
            this.Symbol = symbol;
            int index = symbol.IndexOf(".");
            this.MarketType = (MarketType)Enum.Parse(MarketType.GetType(), symbol.Substring(index+1));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", Type);
            info.AddValue("MarketType", MarketType);
        }
    }
}
