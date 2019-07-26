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
        public void ProcessOrder(Cart cart, User user)
        {
            StringBuilder address = new StringBuilder();
            address.AppendLine(user.UserDetail.FirstName)
    .AppendLine(user.UserDetail.City)
    .AppendLine(user.UserDetail.Zip)
    .AppendLine(user.UserDetail.Street)
    .AppendLine(user.UserDetail.BuildingNr);
            OrderHeader order = new OrderHeader
            {
                CustomerId = 1,
                CreationDate = DateTime.Now,
                ShipAddress = address.ToString(),
                ShipmentMethod = "default",
                Status = "Ordered",
                //TODO Comment = shippingInfo.GiftWrap ? "Gift wrap" : "",
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
                    Product = line.Product,
                    LineTotal = line.Product.Price * line.Quantity - (line.Product.Price * line.Quantity * 0.05M),
                };
                order.OrderDetail.Add(detail);
            }
            repo.SaveOrder(order);
        }
        }     
    }

