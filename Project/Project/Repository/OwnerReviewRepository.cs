using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class OwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/ownerReview.csv";

        private readonly Serializer<OwnerReview> serializer;

        private List<OwnerReview> reviews;

        public OwnerReviewRepository()
        {
            serializer = new Serializer<OwnerReview>();
            reviews = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, reviews);
        }

        private int GenerateId()
        {
            if (reviews.Count == 0) return 0;
            return reviews[reviews.Count - 1].Id + 1;
        }

        public OwnerReview Add(OwnerReview review)
        {
            review.Id = GenerateId();
            reviews.Add(review);
            SaveInFile();
            return review;
        }
        

        public OwnerReview AddOrUpdate(OwnerReview review)
        {
            OwnerReview oldReview = GetReviewByIds(review.Guest1Id, review.OwnerId);
            if (oldReview == null)
            {
                review.Id = GenerateId();
                reviews.Add(review);
                SaveInFile();
                return review;
            }
            oldReview.OwnerId = review.OwnerId;
            oldReview.Guest1Id = review.Guest1Id;
            oldReview.Cleanliness = review.Cleanliness;
            oldReview.HousePolicies = review.HousePolicies;
            oldReview.Comment = review.Comment;
            SaveInFile();
            return oldReview;
        }
        

        public OwnerReview Update(OwnerReview review)
        {
            OwnerReview oldReview = GetReviewById(review.Id);
            if (oldReview == null) return null;

            oldReview.OwnerId = review.OwnerId;
            oldReview.Guest1Id = review.Guest1Id;
            oldReview.Cleanliness = review.Cleanliness;
            oldReview.HousePolicies = review.HousePolicies;
            oldReview.Comment = review.Comment;
            SaveInFile();
            return oldReview;
        }

        public OwnerReview Remove(int id)
        {
            OwnerReview review = GetReviewById(id);
            if (review == null) return null;

            reviews.Remove(review);
            SaveInFile();
            return review;
        }

        public OwnerReview GetReviewById(int id)
        {
            return reviews.Find(v => v.Id == id);
        }

        public OwnerReview GetReviewByIds(int id1, int id2)
        {
            return reviews.Find(v => v.Guest1Id == id1 & v.OwnerId == id2);
        }

        public List<OwnerReview> GetAllReviews()
        {
            return reviews;
        }

    }
}
