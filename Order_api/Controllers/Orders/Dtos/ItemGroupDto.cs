using System;

namespace Order_api.Controllers.Orders.Dtos
{
    public class ItemGroupDto
    {
        public string ItemId { get; set; }
        public int OrderedAmount { get; set; }
        
        public ItemGroupDto WithItemId(Guid itemId)
        {
            ItemId = itemId.ToString("N");
            return this;
        }

        public ItemGroupDto WithOrderedAmount(int orderedAmount)
        {
            OrderedAmount = orderedAmount;
            return this;
        }
    }
}
