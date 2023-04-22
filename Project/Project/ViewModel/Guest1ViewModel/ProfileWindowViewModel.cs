using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.Guest1ViewModel
{
    public class ProfileWindowViewModel : ViewModelBase
    {
        private readonly AccommodationReservationReviewService _reservationReviewService;
        public User User { get; set; }

        public ObservableCollection<Guest1Review> GivenReviews { get; set; }
        public ObservableCollection<OwnerReview> RecievedReviews { get; set; }

        public ProfileWindowViewModel(User u)
        {
            //_reservationReviewService = Injector.Injector.CreateInstance<AccommodationReservationReviewService>();
            _reservationReviewService = new AccommodationReservationReviewService();
            GivenReviews = new ObservableCollection<Guest1Review>();
            RecievedReviews = new ObservableCollection<OwnerReview>();
            User = u;
            _reservationReviewService.FillGuestsGivenAndRecievedReviewsLists(GivenReviews, RecievedReviews, User.Id);
        }
    }
}
