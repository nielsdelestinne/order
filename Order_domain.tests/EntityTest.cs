using System;
using Order_domain.tests.Customers;
using Xunit;

namespace Order_domain.tests
{
    public class EntityTest
    {
        [Fact]
        public void GenerateId_GivenCustomerWithoutId_WhenGeneratingId_ThenGenerateId()
        {
            Entity customer = CustomerTestBuilder.ACustomer().Build();

            Assert.NotEqual(Guid.Empty, customer.Id);
        }
    }
}
