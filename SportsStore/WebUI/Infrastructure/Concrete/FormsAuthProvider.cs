using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        private IUserRepository _user;
        public FormsAuthProvider(IUserRepository user )
        {
            _user = user;
        }

        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
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

        public void RegisterUser(User user)
        {
            this._user.RegisterUser(user);
        }

    }
}