using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Orders
{
    public class OrderRepository : IRepository<Order>
    {

        private readonly DatabaseContext _dBContext;

        public OrderRepository(DatabaseContext dBContext)
        {
            _dBContext = dBContext;
        }

        public Order Save(Order entity)
        {
            _dBContext.Orders.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public Order Update(Order entity)
        {
            _dBContext.SaveChanges();
            return entity;
        }

        public Order Get(Guid entityId)
        {
            return _dBContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Order> GetAll()
        {
            return _dBContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return _dBContext.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .Where(order => order.Customer.Id.Equals(customerId))
                .ToList();
        }
    }
}
