using System;

namespace Order_api.Controllers.Orders.Dtos
{
    public class OrderAfterCreationDto
    {
        public string OrderId { get; set; }
        public float TotalPrice { get; set; }

        public OrderAfterCreationDto WithOrderId(Guid orderId)
        {
            OrderId = orderId.ToString("N");
            return this;
        }

        public OrderAfterCreationDto WithTotalPrice(float totalPrice)
        {
            TotalPrice = totalPrice;
            return this;
        }
    }
}
