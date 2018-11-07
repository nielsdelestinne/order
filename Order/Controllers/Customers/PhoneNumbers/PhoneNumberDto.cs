namespace Order_api.Controllers.Customers.PhoneNumbers
{
    public class PhoneNumberDto
    {
        public string Number { get; private set; }

        public string CountryCallingCode { get; private set; }
        
        public PhoneNumberDto WithNumber(string number)
        {
            Number = number;
            return this;
        }

        public PhoneNumberDto WithCountryCallingCode(string countryCallingCode)
        {
            CountryCallingCode = countryCallingCode;
            return this;
        }
    }
}
