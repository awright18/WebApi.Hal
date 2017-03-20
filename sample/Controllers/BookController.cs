using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using HalSample.Models;

namespace HalSample.Controllers
{
    public class BookController : ApiController
    {
        private readonly IBooks _bookData;

        public BookController()
        {
            _bookData = new InMemoryBooks();
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Book Get(int id)
        {
            var book = _bookData.GetById(id);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return book;
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