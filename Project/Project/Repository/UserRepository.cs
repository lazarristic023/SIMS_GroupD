using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project.Model;
using Project.Serializer;
using Project.RepositoryInterfaces;

namespace Project.Repository
{
    public class UserRepository : IUserRepository
    {

        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, users);
        }

        public User Update(User user)
        {
            User oldUser = GetById(user.Id);
            if (oldUser == null) return null;

            oldUser.Username = user.Username;
            oldUser.Role = user.Role;
            oldUser.Age = user.Age;
            oldUser.Points = user.Points;
            oldUser.Password = user.Password;
            oldUser.SuperUserActivationDate = user.SuperUserActivationDate;

            SaveInFile();
            return oldUser;
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public User GetById(int id)
        {
            users = serializer.FromCSV(FilePath);
            return users.Find(v => v.Id == id);
        }

    }
}
