namespace Order_domain.Customers
{
    public class CustomerRepository : Repository<Customer, CustomerDatabase>
    {
        public CustomerRepository(CustomerDatabase database)
            : base(database)
        {
        }
    }
}
