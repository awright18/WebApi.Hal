using System;
using System.Collections.Generic;

namespace HalSample.Models
{
    public interface IBooks
    {
        Book GetById(int id);
    }

    public class InMemoryBooks : IBooks
    {
        private readonly Dictionary<int, Book> _books;

        public InMemoryBooks()
        {
            _books = new Dictionary<int, Book>();
            InitalizeAuthors();
        }

        public Book GetById(int id)
        {
            Book book;
            var result = _books.TryGetValue(id, out book);

            if (result)
                return book;

            return null;
        }

        private void InitalizeAuthors()
        {
            _books.Add(1, new Book(1, "Ready Player One", DateTime.Now, new Author(1, "Ernest", "Cline")));
            _books.Add(2, new Book(2, "Daivd Copperfield",DateTime.Now,new  Author(2, "Charles", "Dickens")));
            _books.Add(3, new Book(3,"Romeo & Juliet",DateTime.Now, new Author(3, "William", "Shakespeare")));
            _books.Add(4, new Book(4,"Game of Thrones",DateTime.Now, new Author(4, "George R. R.", " Martin")));
            _books.Add(5, new Book(5,"Lord of the Rings", DateTime.Now, new Author(5, "J.R.R.", "Tolkein")));
            _books.Add(6, new Book(6,"Of Mice and Men",DateTime.Now,new  Author(6, "John", "Steinbeck")));
            _books.Add(7, new Book(7,"The Cat in the Hat",DateTime.Now, new Author(7, "Dr.", "Seuss")));
        }
    }

}