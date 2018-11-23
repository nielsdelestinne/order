using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Orders
{
    public class OrderRepository : Repository<Order, OrderDatabase>, IOrderRepository
    {

        public OrderRepository(OrderDatabase database)
            : base(database)
        {
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return Database.GetAll().Where(x => x.Value.CustomerId == customerId).Select(x => x.Value).ToList();
        }
    }
}
