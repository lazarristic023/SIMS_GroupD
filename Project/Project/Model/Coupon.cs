using Project.Controller;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Project.Model
{
    public class Coupon: ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Coupon()
        {
            Id = -1;
            GuestId = -1;
            ExpiryDate = DateTime.Now;
        }

        public Coupon(int guestId, DateTime dateOfExpire)
        {   
            Id = -1;
            GuestId = guestId;
            ExpiryDate = dateOfExpire;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                GuestId.ToString(),
                ExpiryDate.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            ExpiryDate = DateTime.Parse(values[2]);
        }
    }
}
