using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Models.Orders;

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

        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            model.orderHeader = repository.Orders.FirstOrDefault(x => x.Id == id);
            if (model.orderHeader == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit (int? Id)
        {
            var user = _userRepository.GetUserByName(User.Identity.Name);
            string userRole = user.UserRole ?? "default"; 
     
            if (Id != null && userRole.Equals("admin"))
            {
                OrderEditViewModel model = new OrderEditViewModel();
                model.Order = repository.Orders.Where(x => x.Id == Id).SingleOrDefault();
                ViewBag.OrderStatus = new SelectList(repository.OrderStatuses.Where(x=>x.OrderStatusId!=6), "OrderStatusId", "Description", model.Order.OrderStatusId);
                model.OrderId = (int)Id;
                return View(model);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        [HttpPost]
        public ActionResult Edit (OrderEditViewModel order)
        {
            if (ModelState.IsValid)
            {
                OrderHeader Order = repository.Orders.Where(x => x.Id == order.OrderId).SingleOrDefault();
                Order.ModificationDate = DateTime.Now;
                Order.OrderStatusId = int.Parse(order.OrderStatus);
                repository.SaveOrder(Order);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            var user = _userRepository.GetUserByName(User.Identity.Name);
            string userRole = user.UserRole ?? "default";

            if (Id != null && userRole.Equals("admin"))
            {
                OrderDeleteViewModel model = new OrderDeleteViewModel();
                model.Order = repository.Orders.Where(x => x.Id == Id).SingleOrDefault();
                return View(model);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            OrderHeader Order = repository.Orders.Where(x => x.Id == Id).SingleOrDefault();
            Order.ModificationDate = DateTime.Now;
            Order.OrderStatusId = 6;
            repository.SaveOrder(Order);
            return RedirectToAction("List");
        }



    }
}