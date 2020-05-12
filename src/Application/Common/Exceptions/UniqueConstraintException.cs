using System;

namespace SkipperAgency.Application.Common.Exceptions
{
    public class UniqueConstraintException : Exception
    {
        public UniqueConstraintException(string constraintName, object constraintKey)
            : base($"\"{constraintName}\" ({constraintKey}) already exists.")
        {
        }
    }
}
