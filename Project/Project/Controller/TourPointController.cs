using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class TourPointController
    {
        TourPointRepository tourPointRepository { get; set; }

        public TourPointController()
        {
            tourPointRepository = new TourPointRepository();
        }

        public int Create(string name, bool action)
        {

            TourPoint tourPoint = new TourPoint(name,action);
            return tourPointRepository.Add(tourPoint);

        }

        public TourPoint GetById(int id)
        {
            return tourPointRepository.GetById(id);
        }
    }
}
