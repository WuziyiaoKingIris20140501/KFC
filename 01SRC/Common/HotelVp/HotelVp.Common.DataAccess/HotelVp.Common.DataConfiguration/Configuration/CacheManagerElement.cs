using System.Configuration;

namespace HotelVp.Common.Configuration
{
    public sealed class CacheManagerSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty propLocalCache;
        private static readonly ConfigurationProperty propDistributedCache;

        private static ConfigurationPropertyCollection properties;

        static CacheManagerSection()
        {
            propLocalCache = new ConfigurationProperty("localCache", typeof(ProviderElement), null, ConfigurationPropertyOptions.None);
            propDistributedCache = new ConfigurationProperty("distributedCache", typeof(ProviderElement), null, ConfigurationPropertyOptions.None);

            properties = new ConfigurationPropertyCollection();
            properties.Add(propLocalCache);
            properties.Add(propDistributedCache);
        }

        [ConfigurationProperty("localCache")]
        public ProviderElement LocalCache
        {
            get
            {
                return (ProviderElement)base[propLocalCache];
            }
            set
            {
                base[propLocalCache] = value;
            }
        }

        [ConfigurationProperty("distributedCache")]
        public ProviderElement DistributedCache
        {
            get
            {
                return (ProviderElement)base[propDistributedCache];
            }
            set
            {
                base[propDistributedCache] = value;
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