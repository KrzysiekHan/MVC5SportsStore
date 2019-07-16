using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class DbOrderProcessor : IOrderProcessor
    {
        private EFOrderRepository repo = new EFOrderRepository();
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
            OrderHeader order = new OrderHeader
            {
                CustomerId = 1,
                CreationDate = DateTime.Now,
                ShipAddress = address.ToString(),
                ShipmentMethod = "default",
                Status = "Ordered",
                Comment = shippingInfo.GiftWrap ? "Gift wrap" : "",
                TotalDue = cart.ComputeTotalValue(),
                OrderDetail = new List<OrderDetail>()
            };
              foreach (var line in cart.Lines)
            {
                OrderDetail detail = new OrderDetail
                {
                    CreationDate = DateTime.Now,
                    OrderId = order.Id,
                    Quantity = line.Quantity,
                    UnitPrice = line.Product.Price,
                    UnitPriceDiscount = 0.05M,
                    ProductId = line.Product.ProductID,
                    LineTotal = line.Product.Price * line.Quantity - (line.Product.Price * line.Quantity * 0.05M),
                };
                order.OrderDetail.Add(detail);
            }
            repo.SaveOrder(order);
        }
        }     
    }

