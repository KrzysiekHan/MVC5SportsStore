using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    interface IUserRepository
    {
        User GetUser(int id);

        int RegisterUser(User user);
    }
}
