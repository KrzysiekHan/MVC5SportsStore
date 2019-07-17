using Domain.Abstract;
using Domain.Entities;
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
        private IAuthProvider authProvider;

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

        public PartialViewResult _Nav()
        {
            return PartialView();
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDetail userDetail = new UserDetail
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BuildingNr = model.BuildingNr,
                    City = model.City,
                    Country = model.Country,
                    EmailAdress = model.EmailAdress,
                    PhoneNumber = model.PhoneNumber,
                    State = model.State,
                    Street = model.Street,
                    Zip = model.Zip
                    
                };
                User user = new User
                {
                    UserId = 0,
                    Username = model.Username,
                    Password = model.Password,
                    UserDetail = userDetail

                };
                authProvider.RegisterUser(user);
                ViewData["registerStatus"] = "OK";
            } 
            return View();
        }
    }
}