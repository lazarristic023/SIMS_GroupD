using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";

        private readonly Serializer<TourReservation> serializer;

        private List<TourReservation> tourReservations;

        public TourReservationRepository()
        {
            serializer = new Serializer<TourReservation>();
            tourReservations = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourReservations);
        }

        private int GenerateId()
        {
            if (tourReservations.Count == 0) return 0;
            return tourReservations[tourReservations.Count - 1].Id + 1;
        }

        public TourReservation Add(TourReservation tourReservation)
        {
            tourReservation.Id = GenerateId();
            tourReservations.Add(tourReservation);
            SaveInFile();
            return tourReservation;
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            TourReservation oldTourReservation = GetTourReservationById(tourReservation.Id);
            if (oldTourReservation == null) return null;

            oldTourReservation.StartDate = tourReservation.StartDate;
            oldTourReservation.EndDate = tourReservation.EndDate;
            oldTourReservation.GuestId = tourReservation.GuestId;
            oldTourReservation.TourId = tourReservation.TourId;

            SaveInFile();
            return oldTourReservation;
        }

        public TourReservation GetTourReservationById(int id)
        {
            return tourReservations.Find(v => v.Id == id);
        }

        public List<TourReservation> GetAllTourReservations()
        {
            return tourReservations;
        }
    }
}
