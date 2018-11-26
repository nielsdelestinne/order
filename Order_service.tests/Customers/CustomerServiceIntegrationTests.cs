using System;
using System.Linq;
using Newtonsoft.Json;
using Order_domain;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Order_service.Customers;
using Xunit;

namespace Order_service.tests.Customers
{
    public class CustomerServiceIntegrationTests
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceIntegrationTests()
        {
            _customerRepository = new CustomerRepository(new DatabaseContext());
            _customerService = new CustomerService(_customerRepository, new CustomerValidator());
        }

        [Fact]
        public void CreateCustomer()
        {
            Customer customerToCreate = CustomerTestBuilder.ACustomer().Build();

            Customer createdCustomer = _customerService.CreateCustomer(customerToCreate);

            var customer = _customerRepository.Get(customerToCreate.Id);

            Assert.Equal(JsonConvert.SerializeObject(customerToCreate), JsonConvert.SerializeObject(customer));
            Assert.Equal(JsonConvert.SerializeObject(createdCustomer), JsonConvert.SerializeObject(customer));
        }

        [Fact]
        public void GetAllCustomers()
        {
            Customer customer1 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
            Customer customer2 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
            Customer customer3 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());

            var allCustomers = _customerService.GetAllCustomers().ToList();

            Assert.Contains(JsonConvert.SerializeObject(customer1), JsonConvert.SerializeObject(allCustomers));
            Assert.Contains(JsonConvert.SerializeObject(customer2), JsonConvert.SerializeObject(allCustomers));
            Assert.Contains(JsonConvert.SerializeObject(customer3), JsonConvert.SerializeObject(allCustomers));
        }

        [Fact]
        public void GetCustomer()
        {
            _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
            Customer customerToFind = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
            _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
            Customer foundCustomer = _customerService.GetCustomer(customerToFind.Id);

            Assert
                .Equal(
                    JsonConvert.SerializeObject(customerToFind), 
                    JsonConvert.SerializeObject(foundCustomer));
        }
    }
}
