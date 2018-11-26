using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Items
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly DatabaseContext _dBContext;

        public ItemRepository(DatabaseContext dBContext)
        {
            _dBContext = dBContext;
        }

        public Item Save(Item entity)
        {
            _dBContext.Items.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public Item Update(Item entity)
        {
            _dBContext.Items.Update(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public Item Get(Guid entityId)
        {
            return _dBContext.Items
                .AsNoTracking()
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Item> GetAll()
        {
            return _dBContext.Items
                .AsNoTracking()
                .ToList();
        }
    }
}
