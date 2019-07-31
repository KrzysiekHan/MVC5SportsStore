using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebUI.Models.Orders
{
    public class OrderEditViewModel
    {
        public int OrderId { get; set; }
        public OrderHeader Order { get; set; }
        public IEnumerable<SelectListItem> OrderStatusList { get;set; }
        public string OrderStatus { get; set; }
    }
}