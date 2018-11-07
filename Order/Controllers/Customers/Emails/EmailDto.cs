using System;

namespace Order_api.Controllers.Customers.Emails
{
    public class EmailDto
    {
        public string LocalPart { get; private set; }
        public string Domain { get; private set; }
        public string Complete { get; private set; }
        
        public EmailDto WithLocalPart(string localPart)
        {
            LocalPart = localPart;
            return this;
        }

        public EmailDto WithDomain(string domain)
        {
            Domain = domain;
            return this;
        }

        public EmailDto WithComplete(string complete)
        {
            Complete = complete;
            return this;
        }
    }
}
