using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class MoveRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/moveRequests.csv";

        private readonly Serializer<MoveRequest> serializer;

        private List<MoveRequest> requests;

        public MoveRequestRepository()
        {
            serializer = new Serializer<MoveRequest>();
            requests = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, requests);
        }

        private int GenerateId()
        {
            if (requests.Count == 0) return 0;
            return requests[requests.Count - 1].Id + 1;
        }

        public MoveRequest Add(MoveRequest request)
        {
            request.Id = GenerateId();
            requests.Add(request);
            SaveInFile();
            return request;
        }

        public MoveRequest Update(MoveRequest request)
        {
            MoveRequest oldRequest = GetRequestById(request.Id);
            if (oldRequest == null) return null;

            oldRequest.OwnerId = request.OwnerId;
            oldRequest.ReservationId = request.ReservationId;
            oldRequest.GuestId = request.GuestId;
            oldRequest.Status = request.Status;
            oldRequest.OwnerMessage = request.OwnerMessage;
            oldRequest.GuestMessage = request.GuestMessage;
            oldRequest.NewStartDate = request.NewStartDate;
            oldRequest.NewEndDate = request.NewEndDate;


            SaveInFile();
            return oldRequest;
        }

        public MoveRequest Remove(int id)
        {
            MoveRequest request = GetRequestById(id);
            if (request == null) return null;

            requests.Remove(request);
            SaveInFile();
            return request;
        }

        public MoveRequest GetRequestById(int id)
        {
            return requests.Find(v => v.Id == id);
        }

        public List<MoveRequest> GetAllRequests()
        {
            return requests;
        }
    }
}
