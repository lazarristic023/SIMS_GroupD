using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class TourPointsListRepository
    {
        private const string FilePath = "../../../Resources/Data/tourpointslist.csv";

        private readonly Serializer<TourPointsList> serializer;

        private List<TourPointsList> tourPointsLists;

        public TourPointsListRepository()
        {
            serializer = new Serializer<TourPointsList>();
            tourPointsLists = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourPointsLists);
        }

        private int GenerateId()
        {
            if (tourPointsLists.Count == 0)
                return 0;

            return tourPointsLists[tourPointsLists.Count - 1].Id + 1;
        }

        public void Add(TourPointsList tourPointsList)
        {
            tourPointsList.Id = GenerateId();
            tourPointsLists.Add(tourPointsList);
            SaveInFile();

        }

        public void Remove(int id)
        {
            TourPointsList tourPointsList = GetById(id);

            tourPointsLists.Remove(tourPointsList);
            SaveInFile();

        }

        public TourPointsList GetById(int id)
        {
            return tourPointsLists.Find(v => v.Id == id);
        }

        public TourPointsList GetByTourId(int id)
        {
            return tourPointsLists.Find(v => v.TourId == id);
        }

        public List<TourPointsList> GetAll()
        {
            return tourPointsLists;
        }
    }
}
