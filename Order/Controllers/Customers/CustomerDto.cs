using System;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;

namespace Order_api.Controllers.Customers
{
    public class CustomerDto
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public EmailDto Email { get; private set; }
        public AddressDto Address { get; private set; }
        public PhoneNumberDto PhoneNumber { get; private set; }

        public CustomerDto WithId(Guid id)
        {
            Id = id.ToString("N");
            return this;
        }

        public CustomerDto WithFirstname(string firstname)
        {
            FirstName = firstname;
            return this;
        }

        public CustomerDto WithLastname(string lastname)
        {
            LastName = lastname;
            return this;
        }

        public CustomerDto WithEmail(EmailDto email)
        {
            Email = email;
            return this;
        }

        public CustomerDto WithAddress(AddressDto address)
        {
            Address = address;
            return this;
        }

        public CustomerDto WithPhoneNumber(PhoneNumberDto phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }
    }
}
