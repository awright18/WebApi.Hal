using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WebApi.Hal.MediaTypeFormatter
{
    public abstract class EnumerableHalTypeConfiguration<T> : HalTypeConfiguration where T : IEnumerable<T>
    {
        public EnumerableHalTypeConfiguration(string resourceRoute) : base(resourceRoute)
        {
        }
    }

    public abstract class HalTypeConfiguration<T> : HalTypeConfiguration where T : class
    {
        public HalTypeConfiguration(string resourceRoute) : base(resourceRoute)
        {
        }

        protected abstract void AddLinks(T value);

        protected override void AddLinks(dynamic value)
        {
            AddLinks(value);
        }
    }

    public abstract class HalTypeConfiguration
    {
        private List<HalLink> _links;

        protected HalTypeConfiguration(string resourceRoute)
        {
            ResourceRoute = resourceRoute;
            _links = new List<HalLink>();
        }

        public string ResourceRoute { get; }

        public IEnumerable<HalLink> Links {
            get { return _links; }
        }

        public void AddLink(string linkName, string relativePath)
        {
            var path = HalLink.CreateRelativeLink(ResourceRoute, relativePath);

            AddLink(new HalLink(linkName, path));
        }

        public void AddLink(HalLink halLink)
        {
            if (_links.LinkExists(halLink))
                return;

            _links.Add(halLink);
        }

        public void AddLinkTemplate(string linkName, string relativePath)
        {
            var path = HalLink.CreateRelativeTemplatedLink(ResourceRoute, relativePath);

            AddLink(new HalLink(linkName, path, true));
        }

        protected abstract void AddLinks(dynamic value);

        public virtual dynamic CreateHalObject(dynamic value)
        {
            if (value == null)
                return value;

            _links = new List<HalLink>();

            AddLinks(value);

            dynamic linksObject = new ExpandoObject();
            var links = (IDictionary<string, object>) linksObject;

            foreach (var link in Links) links.Add(link.Name, link.ToHalLinkObject());

            dynamic halObject = new ExpandoObject();
            var hal = (IDictionary<string, object>) halObject;

            hal.Add("_links", linksObject);

            foreach (var property in value.GetType().GetProperties())
            {
                var name = property.Name;
                var propertyValue = property.GetValue(value);
                hal.Add(name, propertyValue);
            }

            return halObject;
        }
    }

    public class HalLink
    {
        public HalLink(string name, string relativePath, bool isTemplated = false)
        {
            Name = name;
            RelativePath = relativePath;
            IsTemplated = isTemplated;
        }

        public string Name { get; set; }

        public string RelativePath { get; set; }

        public bool IsTemplated { get; set; }

        public static string CreateRelativeLink(string basePath, string path)
        {
            return basePath + "/" + path;
        }

        public static string CreateRelativeTemplatedLink(string basePath, string path)
        {
            return basePath + path;
        }

        public object ToHalLinkObject()
        {
            dynamic hrefLink = new ExpandoObject();
            var href = (IDictionary<string, object>) hrefLink;
            href.Add("href", RelativePath);

            if (IsTemplated)
                href.Add("templated", true);

            return href;
        }
    }

    public static class HalLinksExtensions
    {
        public static bool LinkExists(this IEnumerable<HalLink> links, HalLink link)
        {
            if (links.FirstOrDefault(l => l.Name == link.Name) != null)
                return true;
            return false;
        }
    }
}