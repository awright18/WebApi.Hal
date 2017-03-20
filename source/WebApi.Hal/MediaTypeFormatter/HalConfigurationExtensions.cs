using System;
using System.Web.Http;

namespace WebApi.Hal.MediaTypeFormatter
{
    public static class HalConfigurationExtensions
    {
        public static void AddHalMediaFormatter(this HttpConfiguration config,
            Action<HalConfigurationCollection> halConfigs)
        {
            var configs = new HalConfigurationCollection();

            halConfigs(configs);

            var halFormatter = new HalMediaTypeFormatter(configs);

            if (config.Formatters.Contains(halFormatter))
                return;

            config.Formatters.Add(halFormatter);
        }
    }
}