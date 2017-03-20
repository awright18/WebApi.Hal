using System;
using System.Collections.Generic;

namespace WebApi.Hal.MediaTypeFormatter
{
    public class HalConfigurationCollection
    {
        private readonly Dictionary<Type, HalTypeConfiguration> _halConfigurations;

        public HalConfigurationCollection()
        {
            _halConfigurations = new Dictionary<Type, HalTypeConfiguration>();
        }

        public bool ContainsConfigurationFor(Type type)
        {
            return _halConfigurations.ContainsKey(type);
        }

        public void AddConfiguration(Type returnType, HalTypeConfiguration halTypeConfiguration)
        {
            if (_halConfigurations.ContainsKey(returnType))
                return;

            _halConfigurations.Add(returnType, halTypeConfiguration);
        }

        public bool TryGetConfigurationFor(Type type, out HalTypeConfiguration configuration)
        {
            return _halConfigurations.TryGetValue(type, out configuration);
        }
    }
}