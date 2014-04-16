using System.Configuration;

namespace HotelVp.Common.Configuration
{
    public sealed class ProvidersSection : ConfigurationSection
    {
        #region Fields

        private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string));
        private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        #endregion

        #region Constructors

        static ProvidersSection()
        {
            _properties.Add(_propProviders);
            _properties.Add(_propDefaultProvider);
        }

        #endregion

        #region Properties
        [StringValidator(MinLength = 1), ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get
            {
                return (string)base[_propDefaultProvider];
            }
            set
            {
                base[_propDefaultProvider] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }

        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base[_propProviders];
            }
        }

        #endregion
    }
}
