using System;

namespace HalSample.Models
{
    public class Book
    {
        public Book()
        {
            
        }

        public Book(int id, string title, DateTime publishedDate, Author author)
        {
            Id = id;
            Title = title;
            PublishedDate = publishedDate;
            Author = author;
        }

        public Author Author { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}