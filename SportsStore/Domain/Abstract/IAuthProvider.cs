using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Abstract
{
   public interface IAuthProvider
    {
        void RegisterUser(User user);

        bool Authenticate(string username, string password);

        void Logout();

        string GetUsername(HttpContext context);
    }
}
