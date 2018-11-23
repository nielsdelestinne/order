using System;

namespace Order_infrastructure.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string additionalContext)
         :base(additionalContext)
        {
        }
    }
}
