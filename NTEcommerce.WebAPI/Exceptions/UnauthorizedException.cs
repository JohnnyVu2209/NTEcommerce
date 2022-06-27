using System;

namespace NTEcommerce.WebAPI.Exceptions
{
    public abstract class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
