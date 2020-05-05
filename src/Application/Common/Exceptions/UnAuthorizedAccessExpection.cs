using System;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnauthorizedException(string resourse, string unauthorizedUser)
            : base($"User {unauthorizedUser} is not authorized to access resource {resourse}.")
        {
        }
    }
}
