using System;
using Order_infrastructure.builders;
using Order_domain.Items.Prices;
using Order_domain.Items;

namespace Order_domain.Orders.OrderItems
{
    public class OrderItem : Entity
    {
        public Item Item { get; set; }
        public Guid ItemId { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public decimal ItemPrice { get; set; }
        public int OrderedAmount { get; set; }
        public DateTime ShippingDate { get; set; }

        private OrderItem(): base(Guid.Empty) { }

        public OrderItem(OrderItemBuilder orderItemBuilder)
            : base(orderItemBuilder.Id)
        {
            ItemId = orderItemBuilder.ItemId;
            ItemPrice = orderItemBuilder.ItemPrice.Amount;
            OrderedAmount = orderItemBuilder.OrderedAmount;
            ShippingDate = CalculateShippingDate(orderItemBuilder.AvailableItemStock);
        }

        private DateTime CalculateShippingDate(int availableItemStock)
        {
            if (availableItemStock - OrderedAmount >= 0)
            {
                return DateTime.Now.AddDays(1);
            }
            return DateTime.Now.AddDays(7);
        }

        public Price GetTotalPrice()
        {
            return Price.Create(ItemPrice * OrderedAmount);
        }

        public override string ToString()
        {
            return "OrderItem{" + "itemId=" + ItemId +
                    ", itemPrice=" + ItemPrice +
                    ", orderedAmount=" + OrderedAmount +
                    ", shippingDate=" + ShippingDate +
                    '}';
        }

        public sealed class OrderItemBuilder : Builder<OrderItem>
        {
            public Guid Id { get; set; }
            public Guid ItemId { get; set; }
            public Price ItemPrice { get; set; }
            public int OrderedAmount { get; set; }
            public int AvailableItemStock { get; set; }

            public static OrderItemBuilder OrderItem()
            {
                return new OrderItemBuilder();
            }

            public override OrderItem Build()
            {
                return new OrderItem(this);
            }

            public OrderItemBuilder WithId(Guid id)
            {
                Id = id;
                return this;
            }

            public OrderItemBuilder WithItemId(Guid itemId)
            {
                ItemId = itemId;
                return this;
            }

            public OrderItemBuilder WithItemPrice(Price itemPrice)
            {
                ItemPrice = itemPrice;
                return this;
            }

            public OrderItemBuilder WithOrderedAmount(int orderedAmount)
            {
                OrderedAmount = orderedAmount;
                return this;
            }

            public OrderItemBuilder WithShippingDateBasedOnAvailableItemStock(int availableItemStock)
            {
                AvailableItemStock = availableItemStock;
                return this;
            }
        }
    }
}
