using System.Collections.Generic;
using System.Linq;

namespace HalSample.Models
{

    public interface IReviews
    {
        Review GetById(int id);
    }

    public class InMemoryReviews : IReviews
    {
        private readonly Dictionary<int, Review> _reviews;

        public InMemoryReviews()
        {
            _reviews = new Dictionary<int, Review>();
            InitializeReviews();
        }

        public Review GetById(int id)
        {
            Review review;
            var result = _reviews.TryGetValue(id, out review);

            if (result)
            {
                return review;
            }

            return null;
        }

        public IEnumerable<Review> GetByBookId(int bookId)
        {
            
            var result = _reviews.Values.Where(r => r.BookId == bookId);

            if (result.Any())
            {
                return result;
            }

            return null;
        }

        private void InitializeReviews()
        {
            _reviews.Add(1, new Review()
            {
                BookId = 1,
                Id = 1,
                Rating = 5,
                ReviewText = "Awesome!",
                User = "SomeGuy"
            });
            _reviews.Add(2, new Review()
            {
                BookId = 1,
                Id = 2,
                Rating = 4,
                ReviewText = "Not bad!",
                User = "SomeOtherGuy"
            });

        }
    }

}