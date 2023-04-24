using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Guest1ReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/guest1Reviews.csv";

        private readonly Serializer<Guest1Review> _serializer;

        private List<Guest1Review> reviews;

        public Guest1ReviewRepository()
        {
            _serializer = new Serializer<Guest1Review>();
            reviews = _serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            _serializer.ToCSV(FilePath, reviews);
        }

        private int GenerateId()
        {
            if (reviews.Count == 0) return 0;
            return reviews[reviews.Count - 1].Id + 1;
        }

        public Guest1Review Add(Guest1Review review)
        {
            review.Id = GenerateId();
            reviews.Add(review);
            SaveInFile();
            return review;
        }

        public Guest1Review Update(Guest1Review review)
        {
            Guest1Review oldReview = GetReviewById(review.Id);
            if (oldReview == null) return null;

            oldReview.Cleanliness = review.Cleanliness;
            oldReview.OwnerBehaviour = review.OwnerBehaviour;
            oldReview.Comment = review.Comment;
            oldReview.ReservationId = review.ReservationId;

            SaveInFile();
            return oldReview;
        }

        public Guest1Review Remove(int id)
        {
            Guest1Review review = GetReviewById(id);
            if (review == null) return null;

            reviews.Remove(review);
            SaveInFile();
            return review;
        }

        public Guest1Review GetReviewById(int id)
        {
            return reviews.Find(v => v.Id == id);
        }

        public List<Guest1Review> GetAllReviews()
        {
            return reviews;
        }
    }
}
