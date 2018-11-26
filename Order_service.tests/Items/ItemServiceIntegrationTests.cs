using System;
using System.Linq;
using Newtonsoft.Json;
using Order_domain;
using Order_domain.Items;
using Order_domain.Items.Prices;
using Order_domain.tests.Items;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemServiceIntegrationTests
    {
        private readonly ItemService _itemService;
        private readonly ItemRepository _itemRepository;

        public ItemServiceIntegrationTests()
        {
            _itemRepository = new ItemRepository(new DatabaseContext());
            _itemService = new ItemService(_itemRepository, new ItemValidator());
        }

        [Fact]
        public void CreateItem()
        {
            Item createdItem = _itemService.CreateItem(ItemTestBuilder.AnItem()
                .WithName("The Martian")
                .WithDescription("A cool book written by a software engineer")
                .WithAmountOfStock(239)
                .WithPrice(Price.Create(new decimal(10.90)))
                .Build());

            Assert.NotNull(createdItem);
            Assert.NotEqual(Guid.Empty, createdItem.Id);
            Assert.Equal("The Martian", createdItem.Name);
            Assert.Equal("A cool book written by a software engineer", createdItem.Description);
            Assert.Equal(239, createdItem.AmountOfStock);
            Assert.Equal(new decimal(10.90), createdItem.Price.Amount);
        }

        [Fact]
        public void GetItem()
        {
            Item createdItem = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());

            Item itemFromDb = _itemService.GetItem(createdItem.Id);

            Assert.NotNull(itemFromDb);
            Assert.Equal(JsonConvert.SerializeObject(createdItem), JsonConvert.SerializeObject(itemFromDb));
        }

        [Fact]
        public void GetAllItems()
        {
            Item createdItem1 = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());
            Item createdItem2 = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());

            var allItems = _itemService.GetAllItems().ToList();

            Assert.Contains(JsonConvert.SerializeObject(createdItem1), JsonConvert.SerializeObject(allItems));
            Assert.Contains(JsonConvert.SerializeObject(createdItem2), JsonConvert.SerializeObject(allItems));
        }
    }
}
