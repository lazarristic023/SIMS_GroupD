using Project.Controller;
using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Tour: ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }

        public string Country { get; set; }
        public string City { get; set; }  
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        //public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        //public string CoverImageUrl { get; set; }
        //public List<int> TourPoints { get; set; }

        public LocationController locationController { get; set; }

        public Tour(int locationId, string name, string description, string language, int maxGuests, int duration)
        {
            Id = -1;
            LocationId = locationId;
            Name = name;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Duration = duration;
            Location = new Location();
            
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
        }

        public Tour(string name,int maxGuests)
        {
            Id = -1;
            LocationId = -1;
            Name = name;
            Description = "";
            Language = "";
            MaxGuests = maxGuests;
            Duration = 0;
        }

        public string GetCountry(int id)
        {
            string country = "";
            LocationController locationController = new LocationController();
            country = locationController.GetCountryById(id);
            return country;
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



        }
    }
}
