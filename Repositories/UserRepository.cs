using System.Linq;
using YellowCarrot.Data;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories
{
    public class UserRepository
    {
        private readonly UserDbContext context;
        public UserRepository(UserDbContext context)
        {
            this.context = context;
        }

        //Attempts to att user to dB - returns bool based on success
        public bool AddUser(User newUser)
        {
            //Checks if username is available
            if (IsUsernameAvailable(newUser.Name))
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        //Gets user from dB by recieved name, returns bool based on result
        private bool IsUsernameAvailable(string newName)
        {
            User? u = context.Users.Where(u => u.Name == newName).FirstOrDefault();
            if (u != null)
            {
                return false;
            }
            return true;
        }

        //Returns user from dB if recieved username and password matches a user
        public User? LoginUser(string userName, string password)
        {
            User? user = context.Users.Where(u => u.Name == userName && u.Password == password).FirstOrDefault();
            return user;
        }

        //Returns name of user by recieved id
        public string GetUserNameFromId(int id)
        {
            User? user = context.Users.Where(u => u.UserId == id).FirstOrDefault();
            return user.Name;
        }
    }
}