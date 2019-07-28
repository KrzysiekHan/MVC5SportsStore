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
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private IUserRepository _userRepository;
        public int PageSize = 4;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            this.repository = orderRepository;
            this._userRepository = userRepository;
        }

        // GET: Order
        public ViewResult List(int page = 1)
        {
            var ordersList = repository.Orders.ToList();
            OrderListViewModel model = new OrderListViewModel
            {
                Orders = repository.Orders
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PaginInfo = new PaginInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Orders.Count()
                }
            };
            return View(model);
        }

        public ActionResult Details (int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            model.orderHeader = repository.Orders.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit (int? Id)
        {
            var user = _userRepository.GetUserByName(User.Identity.Name);
            string userRole = user.UserRole ?? "default"; 
            if (Id != null && userRole.Equals("admin"))
            {
                var model = repository.Orders.Where(x => x.Id == Id).SingleOrDefault();
                return View(model);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        [HttpPost]
        public ActionResult Edit (OrderHeader orderHeader)
        {
            if (ModelState.IsValid)
            {
                repository.SaveOrder(orderHeader);
            }
            return View(orderHeader);
        }

    }
}