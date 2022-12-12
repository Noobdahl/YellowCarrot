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

        public bool AddUser(User newUser)
        {
            if (IsUsernameAvailable(newUser.Name))
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool IsUsernameAvailable(string newName)
        {
            User? u = context.Users.Where(u => u.Name == newName).FirstOrDefault();
            if (u != null)
            {
                return false;
            }
            return true;
        }

        public int LoginUser(string userName, string password)
        {
            User? user = context.Users.Where(u => u.Name == userName && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                return user.UserId;
            }
            return -1;
        }
    }
}