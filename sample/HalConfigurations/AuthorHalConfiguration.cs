using HalSample.Models;
using System.Collections.Generic;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample.HalConfigurations
{
    public class AuthorHalConfiguration : HalTypeConfiguration<Author>
    {
        public AuthorHalConfiguration() : base("/api/authors")
        {
        }

        protected override void AddHalLinks(Author value)
        {
            if (value == null)
                return;

            AddLink("self", value.Id.ToString());
            AddLink("books", $"/books?authorid={value.Id.ToString()}",false);
            AddLinkTemplate("find", "{?id}");
        }
    }

    public class AuthorsHalConfiguration : HalTypeConfiguration<IEnumerable<Author>>
    {
        public AuthorsHalConfiguration() : base("/api/authors")
        {
        }

        protected override void AddHalLinks(IEnumerable<Author> value)
        {
           
        }
    }
}