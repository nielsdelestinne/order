using System;

namespace Order_domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity(Guid id)
        {
            if(!Guid.Empty.Equals(id))
            {
                Id = id;
            } else
            {
                Id = Guid.NewGuid();
            }
        }

    }
}
