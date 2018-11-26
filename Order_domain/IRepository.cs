using Microsoft.EntityFrameworkCore;
using Order_domain.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain
{
    public interface IRepository<T> where T : Entity
    {
        T Save(T entity);
        T Update(T entity);
        IList<T> GetAll();
        T Get(Guid entityId);
    }
}
