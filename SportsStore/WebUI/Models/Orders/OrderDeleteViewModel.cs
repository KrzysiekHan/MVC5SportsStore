using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebUI.Models.Orders
{
    public class OrderDeleteViewModel
    {
        public OrderHeader Order { get; set; }
    }
}