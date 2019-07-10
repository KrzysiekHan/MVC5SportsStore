using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebUI.Infrastructure.Concrete
{
    public class CustomAuthProvider:IAuthProvider
    {
        private IUserRepository _user;
        public CustomAuthProvider(IUserRepository user)
        {
            _user = user;
        }

        public bool Authenticate(string username, string password)
        {
            bool result = _user.AuthenticateUser(new User { UserId = 0, Username = username, Password = password});
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }

        public string GetUsername(HttpContext context)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = context.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = ticket.Name;
            return UserName;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool RegisterUser(User user)
        {
            return this._user.RegisterUser(user);
        }
    }
}