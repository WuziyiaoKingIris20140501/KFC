using System.Configuration;

namespace HotelVp.Common.Configuration
{
    public sealed class GlobalLogManagerSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty propEmitter;
        private static readonly ConfigurationProperty propGlobalRegionName;
        private static readonly ConfigurationProperty propIsDebugEnabled;

        protected static ConfigurationPropertyCollection properties;

        static GlobalLogManagerSection()
        {
            propEmitter = new ConfigurationProperty("emitter", typeof(ProviderElement), null, ConfigurationPropertyOptions.IsRequired);
            propGlobalRegionName = new ConfigurationProperty("globalRegionName", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
            propIsDebugEnabled = new ConfigurationProperty("isDebugEnabled", typeof(bool), false, ConfigurationPropertyOptions.None);
            

            properties = new ConfigurationPropertyCollection();
            properties.Add(propEmitter);
            properties.Add(propGlobalRegionName);
            properties.Add(propIsDebugEnabled);
        }

        [ConfigurationProperty("emitter")]
        public ProviderElement Emitter
        {
            get
            {
                return (ProviderElement)base[propEmitter];
            }
            set
            {
                base[propEmitter] = value;
            }
        }

        [ConfigurationProperty("isDebugEnabled")]
        public bool IsDebugEnabled
        {
            get
            {
                return (bool)base[propIsDebugEnabled];
            }
            set
            {
                base[propIsDebugEnabled] = value;
            }
        }

        [ConfigurationProperty("globalRegionName")]
        public string GlobalRegionName
        {
            get
            {
                return (string)base[propGlobalRegionName];
            }
            set
            {
                base[propGlobalRegionName] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return properties;
            }
        }
    }

    public sealed class LocalLogManagerSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty propLocalRegionName;

        protected static ConfigurationPropertyCollection properties;

        static LocalLogManagerSection()
        {
            propLocalRegionName = new ConfigurationProperty("localRegionName", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

            properties = new ConfigurationPropertyCollection();
            properties.Add(propLocalRegionName);
        }

        [ConfigurationProperty("LocalRegionName")]
        public string LocalRegionName
        {
            get
            {
                return (string)base[propLocalRegionName];
            }
            set
            {
                base[propLocalRegionName] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return properties;
            }
        }
    }
}