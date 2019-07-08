using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<User> Users { get { return context.Users; } }
        public User GetUser(int id)
        {
            User dbEntry = context.Users.Where(x=>x.UserId == id).SingleOrDefault();
            return dbEntry;
        }

        public int RegisterUser(User user)
        {
           if (user.UserId == 0)
            {
                return 1;
                //contr
            }
            return 0;
        }
    }
}
