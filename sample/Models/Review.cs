namespace HalSample.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string User { get; set; }
        public string ReviewText { get; set; }
        public int BookId { get; set; }
    }
}