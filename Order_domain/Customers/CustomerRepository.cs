using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Customers
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly DatabaseContext _dBContext;

        public CustomerRepository(DatabaseContext dBContext)
        {
            _dBContext = dBContext;
        }

        public Customer Save(Customer entity)
        {
            _dBContext.Customers.Add(entity);
            _dBContext.SaveChanges();
            return entity;
        }

        public Customer Update(Customer entity)
        {
            _dBContext.SaveChanges();
            return entity;
        }

        public Customer Get(Guid entityId)
        {
            return _dBContext.Customers
                .AsNoTracking()
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Customer> GetAll()
        {
            return _dBContext.Customers
                .AsNoTracking()
                .ToList();
        }

    }
}
