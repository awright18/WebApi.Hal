using System.Collections.Generic;
using HalSample.Models;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample.HalConfigurations
{
    public class ReviewHalConfiguration : HalTypeConfiguration<Review>
    {
        public ReviewHalConfiguration() : base("api/reviews")
        {

        }

        protected override void AddHalLinks(Review value)
        {
            if (value == null)
            {
                return;
            }

            AddLink("self", value.Id.ToString());
            AddLinkTemplate("find", "{?id}");
        }
    
    }

    public class ReviewsHalConfiguration : HalTypeConfiguration<IEnumerable<Review>>
    {
        public ReviewsHalConfiguration() : base("api/reviews")
        {

        }

        protected override void AddHalLinks(IEnumerable<Review> value)
        {
            if (value == null)
            {
                return;
            }

           // AddLink("self", value.Id.ToString());
            AddLinkTemplate("find", "{?id}");
        }

    }
}