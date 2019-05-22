using HalSample.Models;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample.HalConfigurations
{
    public class CategoryHalConfiguration : HalTypeConfiguration<Category>
    {
        public CategoryHalConfiguration() : base("api/categories")
        {
            
        }

        protected override void AddHalLinks(Category value)
        {
            if (value == null)
            {
                return;
            }

            AddLink("self", value.Id.ToString());
            AddLinkTemplate("find", "{?id}");
        }
    }
}