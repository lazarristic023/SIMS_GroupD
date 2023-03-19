using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class TourPointsListController
    {
        TourPointsListRepository tourPointsListRepository { get; set; }
        TourPointController tourPointController { get; set; }

        public TourPointsListController()
        {
            tourPointsListRepository = new TourPointsListRepository();
            tourPointController = new TourPointController();
        }

        public void Create(int tourId, List<int> pointsId)
        {
            TourPointsList tourPointsList = new TourPointsList(tourId,pointsId);
            tourPointsListRepository.Add(tourPointsList);
        
        }

        public List<string> GetAllPointsByTourId(int id) {
           List<TourPointsList> tourPointsLists = tourPointsListRepository.GetAll();
            List<string> points = new List<string>();

            foreach(TourPointsList tourPointsList in tourPointsLists)
            {
                if(tourPointsList.TourId == id)
                {
                    foreach(int pointid in tourPointsList.PointsId)
                    {
                        points.Add(tourPointController.GetById(pointid).Name);
                    }
                }

            }

            return points;
        
        }

        public List<TourPointsList> GetAll()
        {
            return tourPointsListRepository.GetAll();
        }
    }
}
