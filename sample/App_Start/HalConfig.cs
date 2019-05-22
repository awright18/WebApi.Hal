using System.Collections.Generic;
using HalSample.HalConfigurations;
using HalSample.Models;
using System.Web.Http;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample
{
    public static class HalConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.AddHalMediaFormatter(
                configs =>
                {
                    configs.AddConfiguration(typeof(Author), new AuthorHalConfiguration());
                    configs.AddConfiguration(typeof(Book), new BookHalConfiguraton());
                    configs.AddConfiguration(typeof(Category), new CategoryHalConfiguration());
                    configs.AddConfiguration(typeof(Review), new ReviewHalConfiguration());
                    configs.AddConfiguration(typeof(IEnumerable<Review>), new ReviewsHalConfiguration());
                    configs.AddConfiguration(typeof(IEnumerable<Author>), new AuthorsHalConfiguration());

                });
        }
    }
}