using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    class EFOrderRepository : IOrderRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<OrderHeader> Orders { get {
                return context.OrderHeaders;
            } }

        public OrderHeader DeleteOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(OrderHeader order)
        {
            if (order.Id == 0)
            {
                context.OrderHeaders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
