using System;
using System.Collections.Generic;

namespace Order_domain
{
    public interface IRepository<T, U>
        where T : Entity
        where U : EntityDatabase<T>
    {
        T Save(T entity);

        T Update(T entity);

        Dictionary<Guid, T> GetAll();

        T Get(Guid entityId);
    }
}