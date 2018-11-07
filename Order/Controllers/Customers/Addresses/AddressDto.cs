namespace Order_api.Controllers.Customers.Addresses
{
    public class AddressDto
    {
        public string StreetName { get; private set; }
        public string HouseNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public AddressDto WithStreetName(string streetName)
        {
            StreetName = streetName;
            return this;
        }

        public AddressDto WithHouseNumber(string houseNumber)
        {
            HouseNumber = houseNumber;
            return this;
        }

        public AddressDto WithPostalCode(string postalCode)
        {
            PostalCode = postalCode;
            return this;
        }

        public AddressDto WithCountry(string country)
        {
            Country = country;
            return this;
        }
    }
}
