using HalSample.Models;
using System.Web.Http;

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
     

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var book = _bookData.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
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