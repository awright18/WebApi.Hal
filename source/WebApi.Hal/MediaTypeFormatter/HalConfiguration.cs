using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

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

        protected abstract void AddHalLinks(T value);

        protected override void AddLinks(dynamic value)
        {
            AddHalLinks(value);
        }
    }

    public abstract class HalTypeConfiguration
    {
        private List<Type> _resourceTypes;

        private List<HalLink> _links;

        private List<object> _embeddedResources;

        protected HalTypeConfiguration(string resourceRoute)
        {
            ResourceRoute = resourceRoute;
            _links = new List<HalLink>();
            _resourceTypes = new List<Type>();
        }

        public string ResourceRoute { get; }

        public IEnumerable<HalLink> Links {
            get { return _links; }
        }

        public void AddResourceType<T>()
        {
            _resourceTypes.Add(typeof(T));
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

        public virtual dynamic CreateHalObject(dynamic value,HalConfigurationCollection configs)
        {
            if (value == null)
                return value;

            _links = new List<HalLink>();

            AddLinks(value);

            var linksObject = GetLinksHalObject(_links);

            var emeddedResources = GetEmbeddedResourcesObject(value, configs);

            dynamic halObject = new ExpandoObject();
            var hal = (IDictionary<string, object>) halObject;

            hal.Add("_links", linksObject);

            var resourcesDictionary = (IDictionary<string, object>)emeddedResources;

            if (resourcesDictionary.Keys.Count > 0)
            {
                hal.Add("_embedded", emeddedResources);
            }
            
            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                if (property.IsResourceType(configs))
                {
                    continue;
                }

                var name = property.Name;
                var propertyValue = property.GetValue(value);                
                hal.Add(name, propertyValue);
            }

            return halObject;
        }

        public dynamic GetEmbeddedResourcesObject(dynamic value, HalConfigurationCollection configs)
        {          
            dynamic resourcessObject = new ExpandoObject();
            var resourcesDictionary = (IDictionary<string, object>)resourcessObject;

            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                if (!property.IsResourceType(configs))
                {
                    continue;
                }

                HalTypeConfiguration configuration;
                configs.TryGetConfigurationFor(property.PropertyType, out configuration);

                var resource = property.GetValue(value);

                var halObject = configuration.CreateHalObject(resource, configs);

                var name = property.Name.ToLower();

                resourcesDictionary.Add(name, halObject);
            }

            return resourcessObject;

        }

        public dynamic GetLinksHalObject(IEnumerable<HalLink> links )
        {
            dynamic linksObject = new ExpandoObject();
            var linksDictionary = (IDictionary<string, object>) linksObject;

            foreach (var link in links) linksDictionary.Add(link.Name, link.ToHalLinkObject());
            return linksObject;
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

        public static bool IsResourceType(this PropertyInfo p, HalConfigurationCollection configs)
        {
            return configs.ContainsConfigurationFor(p.PropertyType);
        }
    }
}