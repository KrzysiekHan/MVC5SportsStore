using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    class DbOrderProcessor : IOrderProcessor
    {

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            StringBuilder address = new StringBuilder();
            address.AppendLine(shippingInfo.Name)
    .AppendLine(shippingInfo.Line1)
    .AppendLine(shippingInfo.Line2 ?? "")
    .AppendLine(shippingInfo.Line3 ?? "")
    .AppendLine(shippingInfo.City)
    .AppendLine(shippingInfo.State ?? "")
    .AppendLine(shippingInfo.Country)
    .AppendLine(shippingInfo.Zip);
            OrderHeader orderHeader = new OrderHeader
            {
                CustomerId = 1,
                ShipAddress = address.ToString(),
                ShipmentMethod = "default",
                Status = "Ordered",
                Comment = shippingInfo.GiftWrap ? "Gift wrap" : "",
                TotalDue = cart.ComputeTotalValue()
            };
            var orderDetails = new List<OrderDetail>() {
              foreach (var line in cart.Lines)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = orderHeader.Id,

                });
            }
        }

        
    }
}
