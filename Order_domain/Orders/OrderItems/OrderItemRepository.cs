using Order_domain.Orders.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.OrderItems
{
    public class OrderItemRepository : IRepository<OrderItem>
    {

        private readonly DatabaseContext _dBContext;

        public OrderItemRepository(DatabaseContext dBContext)
        {
            _dBContext = dBContext;
        }

        public OrderItem Save(OrderItem entity)
        {
            _dBContext.OrderItems.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public OrderItem Update(OrderItem entity)
        {
            _dBContext.SaveChanges();
            return entity;
        }

        public OrderItem Get(Guid entityId)
        {
            return _dBContext.OrderItems.Single(item => item.Id.Equals(entityId));
        }

        public IList<OrderItem> GetAll()
        {
            return _dBContext.OrderItems.ToList();
        }

    }
}
