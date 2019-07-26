using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        User GetUser(int id);

        User GetUserByName(string name); 

        bool RegisterUser(User user);

        bool AuthenticateUser(User user);
    }
}
