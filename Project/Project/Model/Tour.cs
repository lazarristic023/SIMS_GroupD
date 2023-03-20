using Project.Serializer;
using System;
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
        public int LocationId { get; set; }

        public Location Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public string CoverImageUrl { get; set; }
        public List<Image> Images { get; set; }
        public List<int> TourPoints { get; set; }
        
        public Tour()
        {
            Images = new List<Image>();
            Location = new Location();
        }

        public Tour(string name, string description, string language, int maxGuests, DateTime startTime, int duration, string coverImgUrl)
        {
            Name = name;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            StartTime = startTime;
            Duration = duration;
            CoverImageUrl = coverImgUrl;
            Images = new List<Image>();
            TourPoints = new List<int>();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Country,Location.City, Description, Language, MaxGuests.ToString(), 
                StartTime.ToString(), Duration.ToString(), CoverImageUrl};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = Convert.ToString(values[1]);
            Location.Country = values[2];
            Location.City = values[3];
            Description = values[4];
            Language = values[5];
            MaxGuests = Convert.ToInt32(values[6]);
            StartTime = DateTime.Parse(values[7]);
            Duration = Convert.ToInt32(values[8]);
            CoverImageUrl = values[9];

        }

        /*
        public Tour(int id, int locationId, string name, string description, string language, int maxGuests, DateTime startTime, int duration, string coverImageUrl)
        {
            Id = id;
            LocationId = locationId;
            Name = name;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            StartTime = startTime;
            Duration = duration;
            CoverImageUrl = coverImageUrl;
            TourPoints = new List<int>();
        }

        public Tour()
        {
            Id = -1;
            LocationId = -1;
            Name = "";
            Description = "";
            Language = "";
            MaxGuests = 0;
            StartTime = DateTime.MinValue;
            Duration = 0;
            CoverImageUrl = "";
            TourPoints = new List<int>();
        }

        public string[] ToCSV() {
            string[] csvValues = { 
                Id.ToString(),
                LocationId.ToString(), 
                Name,
                Description,
                Language,
                MaxGuests.ToString(),
                StartTime.ToString(),
                Duration.ToString(),
                CoverImageUrl,

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
            StartTime = DateTime.Parse(values[6]);
            Duration = int.Parse(values[7]);
            CoverImageUrl = values[8];

        }*/
    }
}
