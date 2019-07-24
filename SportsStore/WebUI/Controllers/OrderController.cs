using Domain.Abstract;
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
        public int PageSize = 4;

        public OrderController(IOrderRepository orderRepository)
        {
            this.repository = orderRepository;
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


    }
}