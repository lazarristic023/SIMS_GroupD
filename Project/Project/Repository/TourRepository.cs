using Project.Serializer;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> serializer;

        private List<Tour> tours;

        public TourRepository(){
            serializer = new Serializer<Tour>();
            tours = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tours);
        }

        private int GenerateId()
        {
            if (tours.Count == 0) 
                return 0;

            return tours[tours.Count - 1].Id + 1;
        }

        public void Add(Tour tour)
        {
            tour.Id = GenerateId();
            tours.Add(tour);
            SaveInFile();
  
        }

        public void Remove(int id)
        {
            Tour tour = GetById(id);

            tours.Remove(tour);
            SaveInFile();

        }

        public Tour GetById(int id)
        {
            return tours.Find(v => v.Id == id);
        }

        public List<Tour> GetAll()
        {
            return tours;
        }


    }
}
