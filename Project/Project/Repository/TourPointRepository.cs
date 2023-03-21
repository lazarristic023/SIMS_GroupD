using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Repository
{
    public class TourPointRepository
    {
        private const string FilePath = "../../../Resources/Data/tourpoint.csv";

        private readonly Serializer<TourPoint> serializer;

        private List<TourPoint> tourPoints;

        public TourPointRepository()
        {
            serializer = new Serializer<TourPoint>();
            tourPoints = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourPoints);
        }

        private int GenerateId()
        {
            if (tourPoints.Count == 0)
                return 0;

            return tourPoints[tourPoints.Count - 1].Id + 1;
        }

        public int Add(TourPoint tourPoint)
        {
            tourPoint.Id = GenerateId();
            tourPoints.Add(tourPoint);
            SaveInFile();
            return tourPoint.Id;

        }

        public void Remove(int id)
        {
            TourPoint tourPoint = GetById(id);

            tourPoints.Remove(tourPoint);
            SaveInFile();

        }

        public TourPoint GetById(int id)
        {
            return tourPoints.Find(v => v.Id == id);
        }

        public List<TourPoint> GetAll()
        {
            return tourPoints;
        }
    }
}
