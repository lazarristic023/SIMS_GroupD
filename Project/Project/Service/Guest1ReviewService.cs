using Project.Injector;
using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class Guest1ReviewService
    {
        private readonly IGuest1ReviewRepository _reviewRepository;
        private readonly IGuest1ReviewImageRepository _imageRepository;

        private readonly AccommodationReservationService _reservationService;

        public Guest1ReviewService()
        {
            _reviewRepository = Injector.Injector.CreateInstance<IGuest1ReviewRepository>();
            _imageRepository = Injector.Injector.CreateInstance<IGuest1ReviewImageRepository>();
            _reservationService = new AccommodationReservationService();
            LinkReviewsAndImages();
            LinkReservationsAndReviews();
        }

        public void Add(Guest1Review review)
        {
            _reviewRepository.Add(review);
            LinkReviewsAndImages();
            LinkReservationsAndReviews();
        }

        public void AddImage(Guest1ReviewImage image)
        {
            _imageRepository.Add(image);
        }

        public List<Guest1Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        private void LinkReviewsAndImages()
        {
            foreach (var image in _imageRepository.GetAllImages())
            {
                var review = _reviewRepository.GetReviewById(image.ReviewId);

                if (review == null)
                {
                    continue;
                }
                if (review.Images.Exists(i => i.Id == image.Id))
                {
                    continue;
                }

                review.Images.Add(image);
            }
        }

        private void LinkReservationsAndReviews()
        {
            foreach (var review in _reviewRepository.GetAllReviews())
            {
                var reservation = _reservationService.GetAllReservations().Find(r => r.Id == review.ReservationId);

                if (reservation != null)
                {
                    reservation.GuestReview = review;
                    review.Reservation = reservation;
                }
            }
        }


    }
}
