using System;
using System.Collections.Generic;
using System.Linq;
using Order_infrastructure.builders;
using Order_domain.Items.Prices;
using Order_domain.Orders.OrderItems;
using Order_domain.Customers;

namespace Order_domain.Orders
{
    public sealed class Order : Entity
    {
        public IList<OrderItem> OrderItems { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        private Order() : base(Guid.Empty) { }

        public Order(OrderBuilder orderBuilder)
            :base(orderBuilder.Id)
        {
            OrderItems = orderBuilder.OrderItems;
            CustomerId = orderBuilder.CustomerId;
        }
        
        public Price GetTotalPrice()
        {
            Price totalPrice = Price.Create(0);
            OrderItems.ToList().ForEach(x => totalPrice = Price.Add(totalPrice, x.GetTotalPrice()));

            return totalPrice;
        }

        public override string ToString()
        {
            return "Order{"
                   + "id=" + Id +
                   ", orderItems=" + OrderItems +
                   ", customerId=" + CustomerId +
                   '}';
        }

        public class OrderBuilder : Builder<Order>
        {
            public Guid Id { get; set; }
            public IList<OrderItem> OrderItems { get; set; }
            public Guid CustomerId { get; set; }
            
            public static OrderBuilder Order()
            {
                return new OrderBuilder();
            }

            public override Order Build()
            {
                return new Order(this);
            }

            public OrderBuilder WithId(Guid id)
            {
                Id = id;
                return this;
            }

            public OrderBuilder WithOrderItems(IList<OrderItem> orderItems)
            {
                OrderItems = orderItems;
                return this;
            }

            public OrderBuilder WithCustomerId(Guid customerId)
            {
                CustomerId = customerId;
                return this;
            }
        }
    }
}
