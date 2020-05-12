using System;
using System.IO;
using System.Text.RegularExpressions;
using FluentValidation;
using SkipperAgency.Domain.Common;

namespace SkipperAgency.Application.Common.ExtensionMethods
{
    public static class CustomFluentValidators
    {
        public static IRuleBuilderOptions<T, string> IsCurrentUser<T>(this IRuleBuilder<T, string> ruleBuilder, string currentUserId)
        {
            return ruleBuilder.Must(userId =>
            {
                if (currentUserId != userId)
                {
                    throw new UnauthorizedAccessException($"Currently logged in user is not authorized for this action.");
                }
                return true;
            });
        }

        public static IRuleBuilderOptions<T, FileModel> ContainsValidBase64Data<T>(this IRuleBuilder<T, FileModel> ruleBuilder)
        {
            return ruleBuilder.Must(file =>
            {
                var b64Data = file.Base64Data;
                b64Data = b64Data.Trim();
                return (b64Data.Length % 4 == 0) && Regex.IsMatch(b64Data, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
            });
        }
    }
}
