using System.Configuration;

namespace HotelVp.Common.Configuration
{
    public sealed class ProviderElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty propProviders;
        private static readonly ConfigurationProperty propDefaultProvider;

        private static ConfigurationPropertyCollection properties;

        static ProviderElement()
        {
            propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.IsRequired);
            propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

            properties = new ConfigurationPropertyCollection();
            properties.Add(propDefaultProvider);
            properties.Add(propProviders);
        }

        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base[propProviders];
            }
            set
            {
                base[propProviders] = value;
            }
        }

        [StringValidator(MinLength = 1), ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get
            {
                return (string)base[propDefaultProvider];
            }
            set
            {
                base[propDefaultProvider] = value;
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