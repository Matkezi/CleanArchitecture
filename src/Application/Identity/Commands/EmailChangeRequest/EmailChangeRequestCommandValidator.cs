using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Identity.Commands.EmailChangeRequest
{
    public class EmailChangeRequestCommandValidator : AbstractValidator<EmailChangeRequestCommand>
    {
        public EmailChangeRequestCommandValidator(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .IsCurrentUserEmail(identityService.GetEmail(currentUserService.UserId));
        }
    }
}