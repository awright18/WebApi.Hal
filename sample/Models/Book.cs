using System;
using System.Collections.Generic;

namespace HalSample.Models
{
    public class Book
    {
        public Book()
        {
            
        }

        public Book(
            int id, 
            string title, 
            DateTime publishedDate, 
            Author author,
            Category category,
            IEnumerable<Review> reviews)
        {
            Id = id;
            Title = title;
            PublishedDate = publishedDate;
            Author = author;
            Category = category;
            Reviews = reviews;
        }

        public Author Author { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
    }
}