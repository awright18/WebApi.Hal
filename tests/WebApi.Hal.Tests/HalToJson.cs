using HalSample.HalConfigurations;
using HalSample.Models;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.Hal.Tests
{
    public class HalToJson
    {
        [Fact]
        public void CanSerializeAuthorToHal()
        {
            var expected =
                @"{
                    ""_links"": {
                    ""self"": ""/author/1"",
                    ""find"": ""/author{?id}""
                    },
                    ""Id"": 1,
                    ""FirstName"": ""Ernest"",
                    ""LastName"": ""Cline""
                }";
            var author = new Author(1, "Ernest", "Cline");

            var config = new AuthorHalConfiguration();

            var value = config.CreateHalObject(author);

            var actual = JsonConvert.SerializeObject(value);

            Assert.Equal(expected.RemoveWhitespace(), actual);
        }
    }
}