using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class UserRepository
    {
        private readonly UserDbContext context;
        public UserRepository(UserDbContext context)
        {
            this.context = context;
        }
        //CRUD
    }
}
