using Project.Controller;
using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Tour: ISerializable
    {
        public enum STATUS { NOTSTARTED,ACTIVE,COMPLETED}
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }  
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public int Duration { get; set; }
        public STATUS Status { get; set; }
        public bool IsNotCanceled { get; set; }


        private readonly LocationController locationController;

        public Tour(Location location, string name, string description, string language, int maxGuests, int duration)
        {
            Id = -1;
            LocationId = location.Id;
            Name = name;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Duration = duration;
            Location = location;
            Country = location.Country;
            City = location.City;
            locationController = new LocationController();
            Status = STATUS.NOTSTARTED;
            IsNotCanceled = true;

        }

        public Tour()
        {
            Id = -1;
            LocationId = -1;
            Name = "";
            Description = "";
            Language = "";
            MaxGuests = 0;
            Duration = 0;
            Location = new Location();
            City = "";
            Country = "";
            locationController = new LocationController();
            Status = STATUS.NOTSTARTED;
            IsNotCanceled = true;
        }


        public string[] ToCSV() {
            string[] csvValues = { 
                Id.ToString(),
                LocationId.ToString(), 
                Name,
                Description,
                Language,
                MaxGuests.ToString(),
                Duration.ToString(),
                Status.ToString(),
                IsNotCanceled.ToString(),

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
            Name = values[2];
            Description = values[3];
            Language = values[4];
            MaxGuests = int.Parse(values[5]);
            Duration = int.Parse(values[6]);
            Location = locationController.GetById(Id);
            string status = values[7];
            switch (status)
            {
                case "ACTIVE":
                    Status = STATUS.ACTIVE;
                    break;
                case "COMPLETED":
                    Status = STATUS.COMPLETED;
                    break;
                default:
                    Status = STATUS.NOTSTARTED;
                    break;
            }
            IsNotCanceled = bool.Parse(values[8]);
        }
    }
}
