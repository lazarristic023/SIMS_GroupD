using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Model
{
    public class TourPoint: ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Action { get; set; }

        public TourPoint()
        {
            Id = -1;
            Name = "";
            Order = 0;
            Action = false;

        }

        public TourPoint(int id, string name, int order, bool action, int tourId)
        {
            Id = id;
            Name = name;
            Order = order;
            Action = action;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Order.ToString(),
                Action.ToString()
               
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Order = int.Parse(values[2]);
            Action = bool.Parse(values[3]);

        }
    }

}
