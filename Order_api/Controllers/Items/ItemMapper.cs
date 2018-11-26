using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_infrastructure.dto;
using Order_domain.Items;
using Order_domain.Items.Prices;

namespace Order_api.Controllers.Items
{
    public class ItemMapper : Mapper<ItemDto, Item>
    {
        public Item ToDomain(Guid itemId, ItemDto itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.Id))
            {
                return ToDomain(itemDto.WithId(itemId));
            }
            return ToDomain(itemDto);
        }

        public Item ToDomainForUpdate(Guid itemId, ItemDto itemDto)
        {
            if (itemId != new Guid(itemDto.Id))
            {
                throw new ArgumentException(
                    "When updating an item, the provided ID in the path should match the ID in the body: " +
                    "ID in path = " + itemId.ToString("N") + ", ID in body = " + itemDto.Id);
            }
            return ToDomainForUpdate(itemDto);

        }

        public override Item ToDomain(ItemDto itemDto)
        {
            return Item.ItemBuilder.Item()
                .WithId(Guid.Empty)
                .WithName(itemDto.Name)
                .WithDescription(itemDto.Description)
                .WithAmountOfStock(itemDto.AmountOfStock)
                .WithPrice(Price.Create(new decimal(itemDto.Price)))
                .Build();
        }

        private Item ToDomainForUpdate(ItemDto itemDto)
        {
            return Item.ItemBuilder.Item()
                .WithId(new Guid(itemDto.Id))
                .WithName(itemDto.Name)
                .WithDescription(itemDto.Description)
                .WithAmountOfStock(itemDto.AmountOfStock)
                .WithPrice(Price.Create(new decimal(itemDto.Price)))
                .Build();
        }

        public override ItemDto ToDto(Item item)
        {
            return new ItemDto()
                .WithId(item.Id)
                .WithName(item.Name)
                .WithDescription(item.Description)
                .WithAmountOfStock(item.AmountOfStock)
                .WithPrice(item.Price.GetAmountAsFloat())
                .WithStockUrgency(item.GetStockUrgency().ToString());
        }
    }
}