using System;
using FluentValidation;

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
    }
}
