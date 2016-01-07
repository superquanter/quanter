using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Market
{
    public class MarketReceiverConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("receiver", IsDefaultCollection = true)]
        public ReceiverElementCollection Receivers
        {
            get
            {
                return (ReceiverElementCollection)base["receiver"];
            }
        }
    }

    public class ReceiverElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ReceiverElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReceiverElement)element).Name;
        }
    }

    public class ReceiverElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return base["name"].ToString();
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return base["type"].ToString();
            }
            set
            {
                base["type"] = value;
            }
        }

        [ConfigurationProperty("threads", IsRequired = false)]
        public int Threads
        {
            get
            {
                return int.Parse(base["threads"].ToString());
            }
            set
            {
                base["threads"] = value;
            }
        }

    }
}
