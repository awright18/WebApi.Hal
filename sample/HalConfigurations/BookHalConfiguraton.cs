using HalSample.Models;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample.HalConfigurations
{
    public class BookHalConfiguraton : HalTypeConfiguration<Book>
    {
        public BookHalConfiguraton() : base("/api/book")
        {
        }

        protected override void AddHalLinks(Book value)
        {
            if (value == null)
                return;

            AddLink("self", value.Id.ToString());
            AddLinkTemplate("find", "{?id}");
        }
    }
}