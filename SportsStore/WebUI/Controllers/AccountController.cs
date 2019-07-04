using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;


        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                } else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
                    return View();
                }
            } else
            {
                return View();
            }
        }

        public ActionResult Logout(string username)
        {

            if (!string.IsNullOrEmpty(username))
            {
                authProvider.Logout();
                LoginViewModel loginViewModel = new LoginViewModel()
                {
                    UserName = username
                };
                return View(loginViewModel);
            }
            else

            return RedirectToAction("List", "Product");

        }


    }
}