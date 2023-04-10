using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class MoveRequestService
    {
        private MoveRequestRepository _requestRepository;

        public MoveRequestService()
        {
            _requestRepository = new MoveRequestRepository();
        }



    }
}
