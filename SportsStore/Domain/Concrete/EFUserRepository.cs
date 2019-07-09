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

        public void RegisterUser(User user)
        {
           if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.Username = user.Username;
                    dbEntry.Password = user.Password;
                }
            }
            context.SaveChanges();

        }
    }
}
