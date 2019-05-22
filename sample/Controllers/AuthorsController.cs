using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using HalSample.Models;

namespace HalSample.Controllers
{
    public class AuthorsController : ApiController
    {
        private readonly IAuthors _authorsData;

        public AuthorsController()
        {
            _authorsData = new InMemoryAuthors();
        }

        [HttpGet]
        public IEnumerable<Author> Get([FromUri] PageInfo page)
        {
            return _authorsData.GetPage(page);
        }

        // GET api/<controller>/5
        public Author Get(int id)
        {
            var author = _authorsData.GetById(id);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return author;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}