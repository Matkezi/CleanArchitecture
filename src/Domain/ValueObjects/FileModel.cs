using System;
using System.Collections.Generic;
using SkipperAgency.Domain.Common;

namespace SkipperAgency.Domain.ValueObjects
{
    public class FileModel : ValueObject
    {
        public FileModel(string nameWithExt, string base64Data)
        {
            NameWithExt = nameWithExt;
            Base64Data = base64Data;
        }

        public string NameWithExt { get; }
        public string Base64Data { get; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return NameWithExt;
            yield return Base64Data;
        }
    }
}
