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
using Microsoft.VisualBasic.ApplicationServices;

namespace Project.Repository
{
    public class UserRepository : IUserRepository
    {

        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<Model.User> serializer;

        private List<Model.User> users;

        public UserRepository()
        {
            serializer = new Serializer<Model.User>();
            users = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, users);
        }

        public Model.User Update(Model.User user)
        {
            Model.User oldUser = GetById(user.Id);
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

        public Model.User ChangePassword(Model.User user, string newPassword)
        {
            Model.User oldUser = GetById(user.Id);
            if (oldUser == null) return null;
            oldUser.Password = newPassword;

            SaveInFile();
            return oldUser;
        }

        public Model.User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public Model.User GetById(int id)
        {
            users = serializer.FromCSV(FilePath);
            return users.Find(v => v.Id == id);
        }

    }
}
