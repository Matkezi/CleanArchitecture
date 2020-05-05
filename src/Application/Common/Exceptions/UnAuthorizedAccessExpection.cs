using System;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException()
            : base()
        {
        }

        public UnAuthorizedException(string message)
            : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnAuthorizedException(string name, object key)
            : base($"Entity \"{name}\" ({key}) unauthorized access.")
        {
        }
    }
}
