using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class AccommodationReservationRepository
    {

        private const string FilePath = "../../../Resources/Data/accReservations.csv";

        private readonly Serializer<AccommodationReservation> serializer;

        private List<AccommodationReservation> accReservations;

        public AccommodationReservationRepository()
        {
            serializer = new Serializer<AccommodationReservation>();
            accReservations = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, accReservations);
        }

        private int GenerateId()
        {
            if (accReservations.Count == 0) return 0;
            return accReservations[accReservations.Count - 1].Id + 1;
        }

        public AccommodationReservation Add(AccommodationReservation accReservation)
        {
            accReservation.Id = GenerateId();
            accReservations.Add(accReservation);
            SaveInFile();
            return accReservation;
        }

        public AccommodationReservation Update(AccommodationReservation accReservation)
        {
            AccommodationReservation oldReservation = GetReservationById(accReservation.Id);
            if (oldReservation == null) return null;

            oldReservation.StartDate = accReservation.StartDate;
            oldReservation.EndDate = accReservation.EndDate;
            oldReservation.GuestId = accReservation.GuestId;
            oldReservation.AccommodationId = accReservation.AccommodationId;


            SaveInFile();
            return oldReservation;
        }

        public AccommodationReservation Remove(int id)
        {
            AccommodationReservation reservation = GetReservationById(id);
            if (reservation == null) return null;

            accReservations.Remove(reservation);
            SaveInFile();
            return reservation;
        }

        public AccommodationReservation GetReservationById(int id)
        {
            return accReservations.Find(v => v.Id == id);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return accReservations;
        }

    }
}
