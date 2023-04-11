using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.View.TourGuideView;
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
        AppointmentService appointmentService;
        TourService tourService;
        UserRepository userRepository;
        public struct ShowingModel
        {
            public string userName;
            public string tourName;
            public DateTime appointment;
            public double avgRating;
            public bool validity;
        }
        public TourReviewService()
        {
            tourReviewRepository = new TourReviewRepository();
            appointmentService = new AppointmentService();
            tourService = new TourService();

            userRepository = new UserRepository();
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

        public List<ReviewDisplay> GetReviewForDisplay()
        {
            List<ReviewDisplay> list = new List<ReviewDisplay>();
            

            List<TourReview> tourReviews = GetAll();

            foreach(TourReview review in tourReviews)
            {
                ReviewDisplay sm = new ReviewDisplay();
                int tourId = appointmentService.GetTourId(review.AppointmentId);
                sm.Id = review.Id;
                sm.tourName = tourService.GetById(tourId).Name;
                sm.userName = userRepository.GetById(review.GuestId).Username;
                sm.appointment = appointmentService.GetById(review.AppointmentId).DateAndTimeOfAppointment;
                sm.avgRating = (review.InterestingRating + review.GuideLanguageRating + review.GuideKnowledgeRating) / 3;
                sm.validity = review.IsValid;
                sm.review = review.ReviewText;
                sm.appointmentId = review.AppointmentId;
                sm.userId = review.GuestId;
                sm.knowledgeRating = review.GuideKnowledgeRating;
                sm.languageRating = review.GuideLanguageRating;
                sm.interestingRating = review.InterestingRating;

                list.Add(sm);
            }


            return list;
        }

        public void MarkAsInvalid(int id)
        {
            tourReviewRepository.MarkAsInvalid(id);
        }

        public void Subscribe(IObserver observer)
        {
            tourReviewRepository.Subscribe(observer);
        }

    }
}
