using System.Web.Http;
using HalSample.HalConfigurations;
using HalSample.Models;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample
{
    public static class HalConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.AddHalMediaFormatter(
                configs => { configs.AddConfiguration(typeof(Author), new AuthorHalConfiguration()); });
        }
    }
}