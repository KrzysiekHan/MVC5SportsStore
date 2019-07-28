using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebUI.Models.Orders
{
    public class OrderEditViewModel
    {
        public IList<ListItem> OrderStatusList { get; set; }
    }
}