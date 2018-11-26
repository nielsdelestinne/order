using System;
using Order_domain;
using Order_domain.Items;
using Order_domain.tests.Items;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemServiceTests
    {
        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _itemService = new ItemService(new ItemRepository(new DatabaseContext()), new ItemValidator());
        }

        [Fact]
        public void createItem_happyPath()
        {
            Item item = ItemTestBuilder.AnItem().Build();

            Item createdItem = _itemService.CreateItem(item);

            Assert.NotNull(createdItem);
        }

        [Fact]
        public void createItem_givenItemThatIsNotValidForCreation_thenThrowException()
        {
            Item item = ItemTestBuilder.AnItem()
                .WithName(string.Empty)
                .Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _itemService.CreateItem(item));
            Assert.Contains("Invalid Item provided for creation", ex.Message);
        }

        [Fact]
        public void updateItem_happyPath()
        {
            Item item = ItemTestBuilder.AnItem().WithId(Guid.NewGuid()).Build();
            item = _itemService.CreateItem(item);

            item.Description = "UpdatedDesc";
            Item updatedItem = _itemService.UpdateItem(item);

            Assert.NotNull(updatedItem);
            Assert.Equal("UpdatedDesc", updatedItem.Description);
        }

        [Fact]
        public void updateItem_givenItemThatIsNotValidForUpdating_thenThrowException()
        {
            Item item = ItemTestBuilder.AnItem()
                .WithAmountOfStock(-1)
                .Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _itemService.UpdateItem(item));
            Assert.Contains("Invalid Item provided for updating", ex.Message);
        }
    }
}
