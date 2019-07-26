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

        public bool AuthenticateUser(User user)
        {
           using (context)
            {
                User dbUser = context.Users.Where(x=>x.Username == user.Username).SingleOrDefault();
                if (dbUser != null)
                {
                    if (dbUser.Password == user.Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }

            }
        }

        public User GetUser(int id)
        {
            User dbEntry = context.Users.Where(x=>x.UserId == id).SingleOrDefault();
            return dbEntry;
        }

        public User GetUserByName(string name)
        {
            User dbEntry = context.Users.Where(x => x.Username == name).SingleOrDefault();
            return dbEntry;
        }

        public bool RegisterUser(User user)
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
            return true;
        }


    }
}
