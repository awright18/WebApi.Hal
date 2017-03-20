using System;

namespace HalSample.Models
{
    public class Book
    {
        public Author Author { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}