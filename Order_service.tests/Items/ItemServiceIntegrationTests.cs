using System;
using System.Collections.Generic;
using System.Text;
using Order_domain.Items;
using Order_domain.Items.Prices;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemServiceIntegrationTests : IDisposable
    {
        private readonly ItemService _itemService;
        private readonly ItemRepository _itemRepository;

        public ItemServiceIntegrationTests()
        {
            _itemRepository = new ItemRepository(new ItemDatabase());
            _itemService = new ItemService(_itemRepository, new ItemValidator());
        }

        public void Dispose()
        {
            _itemRepository.Reset();
        }

        [Fact]
        public void createItem()
        {
            Item createdItem = _itemService.CreateItem(ItemTestBuilder.anItem()
                .withName("The Martian")
                .withDescription("A cool book written by a software engineer")
                .withAmountOfStock(239)
                .withPrice(Price.create(BigDecimal.valueOf(10.90)))
                .build());

            assertThat(createdItem).isNotNull();
            assertThat(createdItem.getId()).isNotNull().isNotEqualTo("");
            assertThat(createdItem.getName()).isEqualTo("The Martian");
            assertThat(createdItem.getDescription()).isEqualTo("A cool book written by a software engineer");
            assertThat(createdItem.getAmountOfStock()).isEqualTo(239);
            assertThat(createdItem.getPrice().getAmount()).isEqualTo(BigDecimal.valueOf(10.90));
        }

        [Fact]
        public void getItem()
        {
            Item createdItem = itemService.createItem(anItem().build());

            Item itemFromDb = itemService.getItem(createdItem.getId());

            assertThat(itemFromDb)
                .isNotNull()
                .isEqualTo(itemFromDb);
        }

        [Fact]
        public void getAllItems()
        {
            Item createdItem1 = itemService.createItem(anItem().build());
            Item createdItem2 = itemService.createItem(anItem().build());

            List<Item> allItems = itemService.getAllItems();

            assertThat(allItems).containsExactlyInAnyOrder(createdItem1, createdItem2);
        }
    }
}
