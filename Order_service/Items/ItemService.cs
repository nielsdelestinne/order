﻿using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain.Items;

namespace Order_service.Items
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;
        private readonly ItemValidator _itemValidator;

        public ItemService(ItemRepository itemRepository, ItemValidator itemValidator)
        {
            _itemRepository = itemRepository;
            _itemValidator = itemValidator;
        }

        public Item CreateItem(Item item)
        {
            if (!_itemValidator.IsValidForCreation(item))
            {
                _itemValidator.ThrowInvalidStateException(item, "creation");
            }
            return _itemRepository.Save(item);
        }

        public Item UpdateItem(Item item)
        {
            if (!_itemValidator.IsValidForUpdating(item))
            {
                _itemValidator.ThrowInvalidStateException(item, "updating");
            }
            return _itemRepository.Update(item);
        }

        public Item GetItem(Guid itemId)
        {
            return _itemRepository.Get(itemId);
        }

        public void DecrementStockForItem(Guid itemId, int amountToDecrement)
        {
            Item item = _itemRepository.Get(itemId);
            item.DecrementStock(amountToDecrement);
            _itemRepository.Update(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemRepository.GetAll().Select(x => x.Value);
        }
    }
}
