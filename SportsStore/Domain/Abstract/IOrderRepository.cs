using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IOrderRepository
    {
        IEnumerable<OrderHeader> Orders { get; }

        void SaveOrder(OrderHeader product);

        OrderHeader DeleteOrder(int orderID);
    }
}
