using Project.Commands;
using Project.Commands.Guest1Commands.WindowHyperlinkCommands;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.Guest1ViewModel
{
    public class ProfileWindowViewModel : ViewModelBase
    {
        private readonly AccommodationReservationReviewService _reservationReviewService;
        public ObservableCollection<Guest1Review> GivenReviews { get; set; }
        public ObservableCollection<OwnerReview> RecievedReviews { get; set; }

        public ProfileWindowViewModel(User u, Window window)
        {
            //_reservationReviewService = Injector.Injector.CreateInstance<AccommodationReservationReviewService>();
            _reservationReviewService = new AccommodationReservationReviewService();
            GivenReviews = new ObservableCollection<Guest1Review>();
            RecievedReviews = new ObservableCollection<OwnerReview>();
            User = u;
            Window = window;
            ProfileLinkCommand = new ProfileHyperlinkCommand(this);
            _reservationReviewService.FillGuestsGivenAndRecievedReviewsLists(GivenReviews, RecievedReviews, User.Id);
        }

        public ICommand ProfileLinkCommand { get; }
    }
}
