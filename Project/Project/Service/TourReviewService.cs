using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourReviewService
    {
        TourReviewRepository tourReviewRepository;
        public TourReviewService()
        {
            tourReviewRepository = new TourReviewRepository();
        }

        public int Create(int appointmentId, int guestId, int knowledge, int language, int interesting,string text )
        {
            TourReview tourReview = new TourReview(appointmentId,guestId,knowledge,language,interesting,text);

            int tourReviewId = tourReviewRepository.Add(tourReview);

            return tourReviewId;

        }

        public TourReview GetById(int id)
        {
            return tourReviewRepository.GetById(id);
        }

        public List<TourReview> GetAll()
        {
            return tourReviewRepository.GetAll();
        }

        public void MarkAsInvalid(int id)
        {
            tourReviewRepository.MarkAsInvalid(id);
        }

    }
}
