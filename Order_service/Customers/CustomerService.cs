using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain.Customers;

namespace Order_service.Customers
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerValidator _customerValidator;
        
        public CustomerService(CustomerRepository customerRepository, CustomerValidator customerValidator)
        {
            _customerRepository = customerRepository;
            _customerValidator = customerValidator;
        }

        public Customer CreateCustomer(Customer customer)
        {
            if (!_customerValidator.IsValidForCreation(customer))
            {
                _customerValidator.ThrowInvalidStateException(customer, "creation");
            }
            return _customerRepository.Save(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll().Select(customer => customer.Value);
        }

        public Customer GetCustomer(Guid id)
        {
            return _customerRepository.Get(id);
        }
    }
}
