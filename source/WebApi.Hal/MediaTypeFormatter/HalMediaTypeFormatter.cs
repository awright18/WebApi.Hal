using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApi.Hal.MediaTypeFormatter
{
    public class HalMediaTypeFormatter : System.Net.Http.Formatting.MediaTypeFormatter
    {
        private const string HalMediaType = "application/hal+json";

        private readonly HalConfigurationCollection _halConfigurations;

        public HalMediaTypeFormatter(HalConfigurationCollection halConfigurations)
        {
            _halConfigurations = halConfigurations;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(HalMediaType));

            SupportedEncodings.Add(new UTF8Encoding(false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return _halConfigurations.ContainsConfigurationFor(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            HalTypeConfiguration configuration;
            _halConfigurations.TryGetConfigurationFor(type, out configuration);

            var hal = configuration.CreateHalObject(value,_halConfigurations);

            var json = JsonConvert.SerializeObject(hal);

            var effectiveEncoding = SelectCharacterEncoding(content.Headers);

            using (var writer = new StreamWriter(writeStream, effectiveEncoding))
            {
                return writer.WriteAsync(json);
            }
        }
    }
}