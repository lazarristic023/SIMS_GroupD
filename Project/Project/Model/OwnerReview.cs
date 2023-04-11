using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class OwnerReview : ISerializable // ovo je review koji vlasnik ostavlja za goste
    {

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int Guest1Id { get; set; }
        public int Cleanliness {get; set; }
        public int HousePolicies { get; set; }
        public string Comment { get; set; }

        public OwnerReview() { }

        public OwnerReview(int ownerId, int guestId, int cleanliness, int housePolicies, string comment)
        {
            OwnerId = ownerId;
            Guest1Id = guestId;
            Cleanliness = cleanliness;
            HousePolicies = housePolicies;
            Comment = comment;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            Guest1Id = Convert.ToInt32(values[2]);
            Cleanliness = Convert.ToInt32(values[3]);
            HousePolicies = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString(), Guest1Id.ToString(), Cleanliness.ToString(), HousePolicies.ToString(), Comment };
            return csvValues;
        }
    }
}
