using System;

namespace SkipperAgency.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object entityKey)
            : base($"\"{entityName}\" ({entityKey}) was not found.")
        {
        }
    }
}
