using System.Collections.Generic;
using System.Linq;

namespace HalSample.Models
{
    public interface IAuthors
    {
        Author GetById(int id);
        IEnumerable<Author> GetPage(PageInfo page);
    }

    public class InMemoryAuthors : IAuthors
    {
        private readonly Dictionary<int, Author> _authors;

        public InMemoryAuthors()
        {
            _authors = new Dictionary<int, Author>();
            InitalizeAuthors();
        }

        public Author GetById(int id)
        {
            Author author;
            var result = _authors.TryGetValue(id, out author);

            if (result)
                return author;

            return null;
        }

        public IEnumerable<Author> GetPage(PageInfo page)
        {
            if (page == null)
            {
                page = new PageInfo();
            }
            var result = _authors.Values.Skip(page.Offset).Take(page.Limit);
            return result;
        }

        private void InitalizeAuthors()
        {
            _authors.Add(1, new Author(1, "Ernest", "Cline"));
            _authors.Add(2, new Author(2, "Charles", "Dickens"));
            _authors.Add(3, new Author(3, "William", "Shakespeare"));
            _authors.Add(4, new Author(4, "George R. R.", " Martin"));
            _authors.Add(5, new Author(5, "J.R.R.", "Tolkein"));
            _authors.Add(6, new Author(6, "John", "Steinbeck"));
            _authors.Add(7, new Author(7, "Dr.", "Seuss"));
        }
    }
}