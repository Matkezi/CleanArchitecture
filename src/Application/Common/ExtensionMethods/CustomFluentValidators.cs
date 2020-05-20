using System;
using System.Text.RegularExpressions;
using FluentValidation;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.ValueObjects;

namespace SkipperAgency.Application.Common.ExtensionMethods
{
    public static class CustomFluentValidators
    {
        public static IRuleBuilderOptions<T, string> IsCurrentUserId<T>(this IRuleBuilder<T, string> ruleBuilder, string currentUserId)
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

        public static IRuleBuilderOptions<T, string> IsCurrentUserEmail<T>(this IRuleBuilder<T, string> ruleBuilder, string currentUserEmail)
        {
            return ruleBuilder.Must(userEmail =>
            {
                if (currentUserEmail != userEmail)
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
                return b64Data.Length % 4 == 0 && Regex.IsMatch(b64Data, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
            });
        }
    }
}
