using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
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
                //context.Set<School>().Add(newItem);
                // use the following statement so that City won't be inserted
                //context.Entry(newItem.City).State = EntityState.Unchanged;

                //context.OrderHeaders.Add(order);
                context.Set<OrderHeader>().Add(order);
                foreach (var item in order.OrderDetail)
                {
                    context.Entry(item.Product).State = EntityState.Unchanged;
                }
                
            }
            context.SaveChanges();
        }
    }
}
