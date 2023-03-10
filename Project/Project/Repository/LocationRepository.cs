using System;
using Project.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> serializer;

        private List<Location> locations;

        public LocationRepository()
        {
            serializer = new Serializer<Location>();
            locations = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, locations);
        }

        private int GenerateId()
        {
            if (locations.Count == 0) return 0;
            return locations[locations.Count - 1].Id + 1;
        }

        public Location Add(Location location)
        {
            location.Id = GenerateId();
            locations.Add(location);
            SaveInFile();
            return location;
        }

        public Location Update(Location location)
        {
            Location oldLocation = GetLocationById(location.Id);
            if (oldLocation == null) return null;

            oldLocation.City = oldLocation.City;
            oldLocation.Country = location.Country;


            SaveInFile();
            return oldLocation;
        }

        public Location Remove(int id)
        {
            Location location = GetLocationById(id);
            if (location == null) return null;

            locations.Remove(location);
            SaveInFile();
            return location;
        }

        public Location GetLocationById(int id)
        {
            return locations.Find(v => v.Id == id);
        }

        public List<Location> GetAllLocations()
        {
            return locations;
        }
    }
}
