﻿using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        private IUserRepository userRepository;
        public User CurrentUser { get; set; }

        public CartController(IProductRepository repo, 
            IOrderProcessor proc,
            IUserRepository userRepo)
        {
            repository = repo;
            orderProcessor = proc;
            userRepository = userRepo;
            this.CurrentUser = new User();
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            User user = new User();
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                user = CurrentUser;
            } else
            {
                user = userRepository.GetUserByName(User.Identity.Name);
            }           
            return View(user);
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart)
        {
            
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }

            if (ModelState.IsValid)
            {
                User user = new User();
                if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    user = CurrentUser;
                }
                else
                {
                    user = userRepository.GetUserByName(User.Identity.Name);
                }
                orderProcessor.ProcessOrder(cart, user);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }
    }
}