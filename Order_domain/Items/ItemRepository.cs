using System;
using System.Collections.Generic;
using System.Text;

namespace Order_domain.Items
{
    public class ItemRepository : Repository<Item, ItemDatabase> {
        
    public ItemRepository(ItemDatabase database)
        :base(database)
    {
    }
    }
}
