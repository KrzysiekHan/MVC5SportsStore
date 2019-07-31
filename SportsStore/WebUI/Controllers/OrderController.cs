using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                OrderEditViewModel model = new OrderEditViewModel();
                model.Order = repository.Orders.Where(x => x.Id == Id).SingleOrDefault();
                ViewBag.OrderStatus = new SelectList(repository.OrderStatuses, "OrderStatusId", "Description", model.Order.OrderStatusId);
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

        public List<SelectListItem> OrderStateList()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem() { Value = "-1", Text = "Wybierz status..." });
            selectListItems.Add(new SelectListItem() { Value = "1", Text = "Oczekujące na płatność" });
            selectListItems.Add(new SelectListItem() { Value = "2", Text = "Kolekcjonowanie zamówienia" });
            selectListItems.Add(new SelectListItem() { Value = "3", Text = "Pakowanie" });
            selectListItems.Add(new SelectListItem() { Value = "4", Text = "Oczekiwanie na kuriera" });
            selectListItems.Add(new SelectListItem() { Value = "5", Text = "Wysłane" });
            return selectListItems;
        }

    }
}