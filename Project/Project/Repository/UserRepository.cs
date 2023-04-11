using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Serializer;

namespace Project.Repository
{
    public class UserRepository
    {

        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public static User GetById(int id)
        { 

            Serializer<User>  serializer = new Serializer<User>();
            List<User> users = new List<User>();
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Id == id);
        }

    }
}
