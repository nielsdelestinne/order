﻿using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain;
using Order_domain.Items;

namespace Order_service.Items
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly ItemValidator _itemValidator;

        public ItemService(IRepository<Item> itemRepository, ItemValidator itemValidator)
        {
            _itemRepository = itemRepository;
            _itemValidator = itemValidator;
        }

        public Item CreateItem(Item item)
        {
            if (!_itemValidator.IsValidForCreation(item))
            {
                _itemValidator.ThrowInvalidOperationException(item, "creation");
            }
            return _itemRepository.Save(item);
        }

        public Item UpdateItem(Item item)
        {
            if (!_itemValidator.IsValidForUpdating(item))
            {
                _itemValidator.ThrowInvalidOperationException(item, "updating");
            }
            MapUpdatedValuesToPersistedItem(item);
            return _itemRepository.Update(item);
        }

        private void MapUpdatedValuesToPersistedItem(Item item)
        {
            var persistedEntity = _itemRepository.Get(item.Id);
            persistedEntity.Name = item.Name;
            persistedEntity.Price = item.Price;
            persistedEntity.Description = item.Description;
            persistedEntity.AmountOfStock = item.AmountOfStock;
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
            return _itemRepository.GetAll();
        }
    }
}
