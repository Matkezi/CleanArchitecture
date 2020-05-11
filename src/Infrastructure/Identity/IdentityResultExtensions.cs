using Microsoft.AspNetCore.Identity;
using SkipperAgency.Application.Common.Models;
using System.Linq;

namespace SkipperAgency.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public static Result ToApplicationResult(this SignInResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.ToString());
        }
    }
}