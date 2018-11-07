using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Order_domain.Customers;
using Xunit;

namespace Order_api.integrationTests.Controllers.Customers
{
    public class CustomerControllerIntegrationTests : IDisposable
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerMapper _customerMapper;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CustomerControllerIntegrationTests()
        {
            _customerRepository = new CustomerRepository(new CustomerDatabase());
            _customerMapper = new CustomerMapper(new AddressMapper(), new EmailMapper(), new PhoneNumberMapper());
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _customerRepository.Reset();
        }

        //[Fact]
        //public async Task CreateCustomer()
        //{
        //    CustomerDto customerToCreate = new CustomerDto()
        //        .WithFirstname("Bruce")
        //        .WithLastname("Wayne")
        //        .WithEmail(new EmailDto()
        //            .WithLocalPart("brucy")
        //            .WithDomain("bat.net")
        //            .WithComplete("brucy@bat.net"))
        //        .WithPhoneNumber(new PhoneNumberDto()
        //            .WithNumber("485212121")
        //            .WithCountryCallingCode("+32"))
        //        .WithAddress(new AddressDto()
        //            .WithStreetName("Secretstreet")
        //            .WithHouseNumber("841")
        //            .WithPostalCode("1238")
        //            .WithCountry("GothamCountry"));

        //    var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/customers")
        //    {
        //        Content = new StringContent(JsonConvert.SerializeObject(customerToCreate), Encoding.UTF8, "application/json")
        //    };

        //    var response = await _client.SendAsync(postRequest);

        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var createdCustomer = JsonConvert.DeserializeObject<CustomerDto>(responseString);

        //    AssertCustomerIsEqualIgnoringId(customerToCreate, createdCustomer);
        //    Assert.Equal(customerToCreate.Id, createdCustomer.Id);
        //}

        //[Fact]
        //public void
        //    createCustomer_givenCustomerNotValidForCreationBecauseOfMissingFirstName_thenErrorObjectReturnedByControllerExceptionHandler()
        //{
        //    CustomerDto customerToCreate = new CustomerDto()
        //        .WithFirstname(null)
        //        .WithLastname("Wayne")
        //        .WithEmail(new EmailDto()
        //            .WithLocalPart("brucy")
        //            .WithDomain("bat.net")
        //            .WithComplete("brucy@bat.net"))
        //        .WithPhoneNumber(new PhoneNumberDto()
        //            .WithNumber("485212121")
        //            .WithCountryCallingCode("+32"))
        //        .WithAddress(new AddressDto()
        //            .WithStreetName("Secretstreet")
        //            .WithHouseNumber("841")
        //            .WithPostalCode("1238")
        //            .WithCountry("GothamCountry"));

        //    ControllerExceptionHandler.Error error = new TestRestTemplate()
        //        .postForObject(format("http://localhost:%s/%s", getPort(), CustomerController.RESOURCE_NAME),
        //            customerToCreate, ControllerExceptionHandler.Error.class);

        //    assertThat(error).isNotNull();
        //    assertThat(error.getHttpStatus()).isEqualTo(HttpStatus.BAD_REQUEST.value());
        //    assertThat(error.getUniqueErrorId()).isNotNull().isNotEmpty();
        //    assertThat(error.getMessage()).contains("Invalid Customer provided for creation. " +
        //                                            "Provided object: Customer{id=");
        //}

        //[Fact]
        //public void getAllCustomers()
        //{
        //    customerRepository.save(CustomerTestBuilder.aCustomer().build());
        //    customerRepository.save(CustomerTestBuilder.aCustomer().build());
        //    customerRepository.save(CustomerTestBuilder.aCustomer().build());

        //    CustomerDto[] allCustomers = new TestRestTemplate()
        //        .getForObject(format("http://localhost:%s/%s", getPort(), CustomerController.RESOURCE_NAME),
        //            CustomerDto[].class);

        //    assertThat(allCustomers).hasSize(3);
        //}

        //[Fact]
        //public void getAllCustomers_assertResultIsCorrectlyReturned()
        //{
        //    Customer customerInDb = customerRepository.save(CustomerTestBuilder.aCustomer().build());

        //    CustomerDto[] allCustomers = new TestRestTemplate()
        //        .getForObject(format("http://localhost:%s/%s", getPort(), CustomerController.RESOURCE_NAME),
        //            CustomerDto[].class);

        //    assertThat(allCustomers).hasSize(1);
        //    assertThat(allCustomers[0])
        //        .isEqualToComparingFieldByFieldRecursively(customerMapper.toDto(customerInDb));
        //}

        //[Fact]
        //public void getCustomer()
        //{
        //    customerRepository.save(CustomerTestBuilder.aCustomer().build());
        //    Customer customerToFind = customerRepository.save(CustomerTestBuilder.aCustomer().build());
        //    customerRepository.save(CustomerTestBuilder.aCustomer().build());

        //    CustomerDto foundCustomer = new TestRestTemplate()
        //        .getForObject(
        //            format("http://localhost:%s/%s/%s", getPort(), CustomerController.RESOURCE_NAME,
        //                customerToFind.getId().toString()), CustomerDto.class);

        //    assertThat(foundCustomer)
        //        .isEqualToComparingFieldByFieldRecursively(customerMapper.toDto(customerToFind));
        //}

        private void AssertCustomerIsEqualIgnoringId(CustomerDto customerToCreate, CustomerDto createdCustomer)
        {
            Assert.False(string.IsNullOrWhiteSpace(createdCustomer.Id));

            Assert.Equal(customerToCreate.Id, createdCustomer.Id);
            Assert.Equal(customerToCreate.FirstName, createdCustomer.FirstName);
            Assert.Equal(customerToCreate.LastName, createdCustomer.LastName);

            Assert.Equal(customerToCreate.Address.StreetName, createdCustomer.Address.StreetName);
            Assert.Equal(customerToCreate.Address.HouseNumber, createdCustomer.Address.HouseNumber);
            Assert.Equal(customerToCreate.Address.PostalCode, createdCustomer.Address.PostalCode);
            Assert.Equal(customerToCreate.Address.Country, createdCustomer.Address.Country);

            Assert.Equal(customerToCreate.Email.LocalPart, createdCustomer.Email.LocalPart);
            Assert.Equal(customerToCreate.Email.Domain, createdCustomer.Email.Domain);
            Assert.Equal(customerToCreate.Email.Complete, createdCustomer.Email.Complete);

            Assert.Equal(customerToCreate.PhoneNumber.Number, createdCustomer.PhoneNumber.Number);
            Assert.Equal(customerToCreate.PhoneNumber.CountryCallingCode, createdCustomer.PhoneNumber.CountryCallingCode);
        }
    }
}
