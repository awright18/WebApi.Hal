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
        private InMemoryReviews _reviews;
        public InMemoryBooks()
        {
            _books = new Dictionary<int, Book>();
            _reviews = new InMemoryReviews();
            InitializeBooks();
        }

        public Book GetById(int id)
        {
            Book book;
            var result = _books.TryGetValue(id, out book);

            if (result)
                return book;

            return null;
        }

        private void InitializeBooks()
        {
            _books.Add(1, new Book(1, "Ready Player One", DateTime.Now, new Author(159, "Ernest", "Cline"), new Category(42,"Science Fiction"),_reviews.GetByBookId(1)));
            _books.Add(2, new Book(2, "Daivd Copperfield",DateTime.Now,new Author(2, "Charles", "Dickens"),new Category(41,"Classic Literature"),null));
            _books.Add(3, new Book(3,"Romeo & Juliet",DateTime.Now, new Author(3, "William", "Shakespeare"), new Category(40, "Dramas and Plays"),null));
            _books.Add(4, new Book(4,"Game of Thrones",DateTime.Now, new Author(4, "George R. R.", " Martin"), new Category(39, "Epic Fantasy"),null));
            _books.Add(5, new Book(5,"Lord of the Rings", DateTime.Now, new Author(5, "J.R.R.", "Tolkein"), new Category(39, "Epic Fantasy"),null));
            _books.Add(6, new Book(6,"Of Mice and Men",DateTime.Now,new  Author(6, "John", "Steinbeck"), new Category(41, "Classic Literature"),null));
            _books.Add(7, new Book(7,"The Cat in the Hat",DateTime.Now, new Author(7, "Dr.", "Seuss"), new Category(38,"Children's Classics"),null));
        }
    }

}